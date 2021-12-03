using BitPack.Core.Shared;
using BitPack.MVVM.ViewModel.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;

namespace BitPack.MVVM.ViewModel
{
	class MainViewModel : ObservableObject
	{
		public ThemeWatcher Watcher { get; set; }

		public RelayCommand DiscoverViewCommand { get; set; }
		public RelayCommand CategoryViewCommand { get; set; }
		public RelayCommand SearchViewCommand { get; set; }
		public RelayCommand DownloadViewCommand { get; set; }
		public RelayCommand SettingsViewCommand { get; set; }

		public DiscoverViewModel DiscoverVM { get; set; }
		public CategoryViewModel CategoryVM { get; set; }
		public SearchViewModel SearchVM { get; set; }
		public DownloadViewModel DownloadVM { get; set; }
		public SettingsViewModel SettingsVM { get; set; }

		private AboutPopupViewModel AboutPopup { get; set; }
		private UpdateAvailablePopupViewModel UpdateAvailablePopup { get; set; }
		private UpdateDownloadPopupViewModel UpdateDownloadPopup { get; set; }
		private UpdateFailedPopupViewModel UpdateFailedPopup { get; set; }

		private object _currentView;

		private Timer updateTimer;
		private BasePopupViewModel _currentPopup = null;
		private List<BasePopupViewModel> _popupQueue = new();

		public bool ViewModelsInitialized = false;
		public bool InterruptUpdate = false;

		public object CurrentView
		{
			get { return _currentView; }
			set
			{
				_currentView = value;
				OnPropertyChanged();
			}
		}

		public BasePopupViewModel CurrentPopup
		{
			get { return _currentPopup; }
			set
			{
				_currentPopup = value;
				OnPropertyChanged();
			}
		}

		public List<BasePopupViewModel> PopupQueue
		{
			get { return _popupQueue; }
			set
			{
				_popupQueue = value;
				OnPropertyChanged();
			}
		}

		public MainViewModel()
		{
			SettingsHelper.Init();
			Watcher = new();

			ViewModelManager.ViewModels.Add("Main", this);
			InitPopupViewModels();

			DiscoverViewCommand = new RelayCommand(o => SetView(DiscoverVM)); ;
			CategoryViewCommand = new RelayCommand(o => SetView(CategoryVM));
			SearchViewCommand = new RelayCommand(o => SetView(SearchVM));
			DownloadViewCommand = new RelayCommand(o => SetView(DownloadVM));
			SettingsViewCommand = new RelayCommand(o => SetView(SettingsVM));

			//TrackingDataHelper.LoadData();
			SettingsHelper.LoadSettings();

			UpdateHelper.CheckUpdateAsync();

			updateTimer = new(UpdateTimerCallback);
			DateTime now = DateTime.Now.ToLocalTime();
			DateTime midnight = DateTime.Today.ToLocalTime();

			if (now > midnight) midnight = midnight.AddDays(1).ToLocalTime();
			int msUntilMidnight = (int)(midnight - now).TotalMilliseconds;
			updateTimer.Change(msUntilMidnight, Timeout.Infinite);

			Update();
		}

		private void InitViewModels()
		{
			if (InterruptUpdate) return;

			DiscoverVM = new DiscoverViewModel();
			CategoryVM = new CategoryViewModel();
			SearchVM = new SearchViewModel();
			DownloadVM = new DownloadViewModel();
			SettingsVM = new SettingsViewModel();

			ViewModelManager.ViewModels.Add("Discover", DiscoverVM);
			ViewModelManager.ViewModels.Add("Category", CategoryVM);
			ViewModelManager.ViewModels.Add("Search", SearchVM);
			ViewModelManager.ViewModels.Add("Download", DownloadVM);
			ViewModelManager.ViewModels.Add("Settings", SettingsVM);

			CurrentView = DiscoverVM;
			ViewModelsInitialized = true;
		}

		private void InitPopupViewModels()
		{
			AboutPopup = new AboutPopupViewModel();
			ViewModelManager.ViewModels.Add("AboutPopup", AboutPopup);

			UpdateAvailablePopup = new UpdateAvailablePopupViewModel();
			ViewModelManager.ViewModels.Add("UpdateAvailablePopup", UpdateAvailablePopup);

			UpdateDownloadPopup = new UpdateDownloadPopupViewModel();
			ViewModelManager.ViewModels.Add("UpdateDownloadPopup", UpdateDownloadPopup);

			UpdateFailedPopup = new UpdateFailedPopupViewModel();
			ViewModelManager.ViewModels.Add("UpdateFailedPopup", UpdateFailedPopup);
		}

		public void SetView(object view)
		{
			CurrentView = view;
		}

		public void Update()
		{
			if (InterruptUpdate) return;
			if (!ViewModelsInitialized) InitViewModels();

			//DiscoverVM.Update();
			//CategoryVM.Update();
			//SearchVM.Update();
		}

		public void QueuePopup(BasePopupViewModel popup)
		{
			PopupQueue.Add(popup);
			CurrentPopup = PopupQueue.Last();
			popup.IsOpen = true;
		}

		public void DequeuePopup(BasePopupViewModel popup)
		{
			PopupQueue.Remove(popup);

			if (PopupQueue.Count == 0) CurrentPopup = null;
			else CurrentPopup = PopupQueue.Last();

			popup.IsOpen = false;
		}

		public void OnPopupBorderClick()
		{
			if (PopupQueue.Count != 0 && PopupQueue.Last().CanCancel == false) return;
			DequeuePopup(PopupQueue.Last());
		}



		public void UpdateTimerCallback(object state)
		{
			Application.Current.Dispatcher.Invoke(delegate { Update(); });
		}



		public void Destroy()
		{
			Watcher.Destroy();
		}
	}
}
