using System;
using System.Linq;
using System.Threading;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CodeGenerater.Translation
{
	public class PluginManager : Pipeline, INotifyPropertyChanged
	{
		#region INotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;

		protected void Notify(string PropertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
		}
		#endregion

		#region Constructor
		#endregion

		#region Field
		ObservableCollection<Plugin> PluginCollection;
		#endregion

		#region Property
		#endregion

		#region Method
		public void Add(Plugin Plugin)
		{

		}

		public void Remove(Plugin Plugin)
		{

		}

		public void MoveUp(Plugin Plugin)
		{

		}

		public void MoveDown(Plugin Plugin)
		{

		}
		#endregion

		#region Helper
		void CreatePipeline()
		{

		}
		#endregion
	}
}