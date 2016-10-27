using System;
using System.IO;
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
		}
		#endregion

		#region Constructor
		public LoadedAssembly(string AssemblyPath)
		{
			_Assembly = Assembly.LoadFile(AssemblyPath);
			var Stream = File.Open(AssemblyPath, FileMode.Open);

			if (Stream.Length > int.MaxValue)
				throw new ArgumentException("Too long to read", "AssemblyPath");


			Stream.Position = 0;
			Bytes = new byte[Stream.Length];
			Stream.Read(Bytes, 0, (int)Stream.Length);
		}
		#endregion

		#region Field
		Assembly _Assembly;
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
			get
			{
				if (_Assembly == null)
					_Assembly = Assembly.Load(Bytes);

				return _Assembly;
			}
		}

		public IEnumerable<Type> Plugins
		{
			get
			{
				return Assembly.GetPlugins();
			}
		}
		#endregion
	}
}