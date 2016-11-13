using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CodeGenerater.Translation
{
	[Serializable]
	public class LoadedAssembly : ISerializable
	{
		#region ISerializable
		public virtual void GetObjectData(SerializationInfo Info, StreamingContext Context)
		{
			SerializationHelper.Serialize(this, Info);
		}

		LoadedAssembly(SerializationInfo Info, StreamingContext Context)
		{
			SerializationHelper.Deserialize(this, Info);
			Assembly = Assembly.Load(Bytes);
		}
		#endregion

		#region Constructor
		public LoadedAssembly(string AssemblyPath)
		{
			Assembly = Assembly.LoadFile(AssemblyPath);
			var Stream = File.Open(AssemblyPath, FileMode.Open);

			if (Stream.Length > int.MaxValue)
				throw new ArgumentException("Too long to read", "AssemblyPath");

			Stream.Position = 0;
			Bytes = new byte[Stream.Length];
			Stream.Read(Bytes, 0, (int)Stream.Length);
		}

		public LoadedAssembly(byte[] AssemblyBytes)
		{
			Bytes = AssemblyBytes.ToArray();
			Assembly = Assembly.Load(Bytes);
		}
		#endregion

		#region Field
		Assembly _Assembly;

		IEnumerable<PluginType> PluginCollection;
		#endregion

		#region Property
		[SerializationTarget]
		public byte[] Bytes
		{
			private set;
			get;
		}

		public Assembly Assembly
		{
			set
			{
				_Assembly = value;

				PluginCollection = (from t in Assembly.GetPlugins() select new PluginType(t)).ToArray();
			}
			get
			{
				if (_Assembly == null)
					Assembly = Assembly.Load(Bytes);

				return _Assembly;
			}
		}

		public IEnumerable<PluginType> Plugins
		{
			get
			{
				return PluginCollection;
			}
		}
		#endregion

		#region Method
		public static bool IsLoadable(Assembly A)
		{
			return (from t in A.DefinedTypes where t.IsInherite(typeof(Plugin)) select t).Count() > 0;
		}

		public static bool IsLoadable(string AssemblyPath)
		{
			try { return IsLoadable(Assembly.LoadFile(AssemblyPath)); }
			catch (BadImageFormatException) { return false; }
		}
		#endregion

		#region Helper
		#endregion
	}
}