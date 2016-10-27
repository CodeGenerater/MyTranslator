using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Reflection;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.Serialization;

namespace CodeGenerater.Translation
{
	[Serializable]
	public class MyTranslator : ISerializable, INotifyPropertyChanged
	{
		#region ISerializable
		protected MyTranslator(SerializationInfo Info, StreamingContext Context)
		{
			SerializationHelper.Deserialize(this, Info);
		}
		
		public void GetObjectData(SerializationInfo Info, StreamingContext Context)
		{
			SerializationHelper.Serialize(this, Info);
		}
		#endregion

		#region INotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;

		protected void Notify(string PropertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
		}
		#endregion

		#region Constructor
		MyTranslator()
		{
			_AssemblyCollection = new ObservableCollection<LoadedAssembly>();
			_PluginCollection = new ObservableCollection<Plugin>();
		}
		#endregion

		#region Field
		[SerializationTarget]
		ObservableCollection<LoadedAssembly> _AssemblyCollection;

		[SerializationTarget]
		ObservableCollection<Plugin> _PluginCollection;

		Pipeline Pipeline;
		#endregion

		#region Property
		public IEnumerable<LoadedAssembly> AssemblyCollection
		{
			private set
			{
				if (_AssemblyCollection != value)
				{
					_AssemblyCollection = (ObservableCollection<LoadedAssembly>)value;
					Notify("AssemblyCollection");
				}
			}
			get
			{
				return _AssemblyCollection;
			}
		}

		public IEnumerable<Plugin> PluginCollection
		{
			private set
			{
				if (_PluginCollection != value)
				{
					_PluginCollection = (ObservableCollection<Plugin>)value;
					Notify("PluginCollection");
					AsPipeline();
				}
			}
			get
			{
				return _PluginCollection;
			}
		}
		#endregion

		#region Method
		public void LoadAssembly(string Path)
		{
			_AssemblyCollection.Add(new LoadedAssembly(Path));
		}

		public void Save()
		{
			DataIntergration.Save(this);
		}

		public static MyTranslator Load()
		{
			return (MyTranslator)DataIntergration.Load();
		}
		#endregion

		#region Helper
		void AsPipeline()
		{
			if (_PluginCollection == null || _PluginCollection.Count == 0)
				return;

			Pipeline?.Dispose();
			Pipeline = new Pipeline(PluginCollection);
		}

		void Initialize()
		{
			_PluginCollection.CollectionChanged += PluginCollection_CollectionChanged;
			AsPipeline();
		}
		#endregion

		#region Event Handler
		private void PluginCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			AsPipeline();
		}
		#endregion
	}
}