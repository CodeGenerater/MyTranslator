using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace CodeGenerater.Translation
{
	[Serializable]
	public class MyTranslator : ISerializable
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

		#region Constructor
		MyTranslator()
		{
			AssemblyCollection = new ObservableCollection<LoadedAssembly>();
			PluginCollection = new ObservableCollection<Plugin>();
		}
		#endregion

		#region Field
		[SerializationTarget]
		ObservableCollection<LoadedAssembly> AssemblyCollection;
		#endregion

		#region Property
		public IEnumerable<LoadedAssembly> Assemblys
		{
			get
			{
				return AssemblyCollection;
			}
		}

		public ObservableCollection<Plugin> PluginCollection
		{
			private set;
			get;
		}
		#endregion

		#region Method
		public void LoadAssembly(string Path)
		{
			AssemblyCollection.Add(new LoadedAssembly(Path));
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
	}
}