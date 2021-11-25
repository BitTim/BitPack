using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitPack.MVVM.ViewModel
{
	public class MainViewModel
	{
		public MainViewModel()
		{
			ViewModelManager.ViewModels.Add("Main", this);
		}

		public void Destroy()
		{
			//Watcher.Destroy();
		}
	}
}
