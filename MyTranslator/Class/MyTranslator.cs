using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace CodeGenerater.Translation
{
	[Serializable]
	public class MyTranslator : ISerializable
	{
		#region ISerializable
		public virtual void GetObjectData(SerializationInfo Info, StreamingContext Context)
		{
			SerializationHelper.Serialize(this, Info);
		}

		MyTranslator(SerializationInfo Info, StreamingContext Context)
		{
			SerializationHelper.Deserialize(this, Info);
		}
		#endregion

		#region Constructor
		#endregion

		#region Field
		[SerializationTarget]
		ObservableCollection<Type> PluginCollection;

		[SerializationTarget]
		ObservableCollection<Plugin> UsingPluginCollection;
		#endregion

		#region Property

		#endregion

		#region Method
		#endregion

		#region Helper
		IEnumerable<Type> GetPluginTypes(Assembly A)
		{
			return from t in A.DefinedTypes
				   where t.IsInherite(typeof(Plugin))
				   select t;
		}
		#endregion
	}
}