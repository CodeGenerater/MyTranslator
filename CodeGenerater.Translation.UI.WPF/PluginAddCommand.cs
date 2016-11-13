using System;
using System.Windows.Input;

namespace CodeGenerater.Translation.UI.WPF
{
	class PluginAddCommand : ICommand
	{
		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			var VM = App.Current.FindResource("MainViewModel") as MainViewModel;
			

		}
	}
}