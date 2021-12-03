using System.Collections.Generic;

namespace BitPack.MVVM.ViewModel
{
	public class ViewModelManager
	{
		private static Dictionary<string, object> _viewModels = new();
		public static Dictionary<string, object> ViewModels { get => _viewModels; set => _viewModels = value; }
	}
}
