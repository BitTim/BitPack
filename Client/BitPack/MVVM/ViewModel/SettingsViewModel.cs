using BitPack.Core.Shared;
using BitPack.Core.Specific;
using BitPack.MVVM.ViewModel.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitPack.MVVM.ViewModel
{
	class SettingsViewModel : ObservableObject
	{
		private bool NoUpdate = true;

		private AboutPopupViewModel AboutPopup { get; set; }
		private MainViewModel MainVM { get; set; }
		public RelayCommand ThemeButtonCommand { get; set; }
		public RelayCommand AccentButtonCommand { get; set; }
		public RelayCommand OnAboutClicked { get; set; }
		public RelayCommand OnDefaultsClicked { get; set; }

		public List<string> Channels => Constants.Channels;

		private string _theme;
		private string _accent;

		private string _username;
		private string _channel;
		private bool _autoCheckUpdates;

		public string Theme
		{
			get => _theme;
			set
			{
				_theme = value;
				OnPropertyChanged();
			}
		}
		public string Accent
		{
			get => _accent;
			set
			{
				_accent = value;
				OnPropertyChanged();
			}
		}

		public string Username
		{
			get => _username;
			set
			{
				if (_username == value) return;

				_username = value;
				SettingsHelper.Data.Username = value;
				OnPropertyChanged();
				if (!NoUpdate) SettingsHelper.CallUpdate();
			}
		}
		
		public string Channel
		{
			get => _channel;
			set
			{
				if (_channel == value) return;

				_channel = value;
				SettingsHelper.Data.Channel = value;
				OnPropertyChanged();
				if (!NoUpdate) SettingsHelper.CallUpdate();
			}
		}

		public bool AutoCheckUpdates
		{
			get => _autoCheckUpdates;
			set
			{
				if (_autoCheckUpdates == value) return;

				_autoCheckUpdates = value;
				SettingsHelper.Data.AutoCheckUpdates = value;
				OnPropertyChanged();
				if (!NoUpdate) SettingsHelper.CallUpdate();
			}
		}

		public SettingsViewModel()
		{
			MainVM = (MainViewModel)ViewModelManager.ViewModels["Main"];
			AboutPopup = (AboutPopupViewModel)ViewModelManager.ViewModels["AboutPopup"];

			ThemeButtonCommand = new RelayCommand(theme => SetTheme((string)theme));
			AccentButtonCommand = new RelayCommand(accent => SetAccent((string)accent));

			OnDefaultsClicked = new RelayCommand(o =>
			{
				SettingsHelper.Data.SetDefault();
				SettingsHelper.ApplyVisualSettings();
				SettingsHelper.CallUpdate();
			});
			OnAboutClicked = new RelayCommand(o =>
			{
				MainVM.QueuePopup(AboutPopup);
			});

			Update();
			NoUpdate = false;
		}

		public void Update()
		{
			Theme = SettingsHelper.Data.Theme;
			Accent = SettingsHelper.Data.Accent;

			Username = SettingsHelper.Data.Username;
			Channel = SettingsHelper.Data.Channel;
			AutoCheckUpdates = SettingsHelper.Data.AutoCheckUpdates;
		}

		public void SetTheme(string theme)
		{
			SettingsHelper.Data.Theme = theme;
			SettingsHelper.ApplyVisualSettings();
			SettingsHelper.CallUpdate();
		}

		public void SetAccent(string accent)
		{
			SettingsHelper.Data.Accent = accent;
			SettingsHelper.ApplyVisualSettings();
			SettingsHelper.CallUpdate();
		}
	}
}
