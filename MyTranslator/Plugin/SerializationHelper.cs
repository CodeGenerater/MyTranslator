using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace CodeGenerater.Translation
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
	public sealed class SerializationTargetAttribute : Attribute
	{

	}

	public static class SerializationHelper
	{
		#region Field
		static BinaryFormatter BF = new BinaryFormatter();
		#endregion

		#region Method
		public static void Serialize(object Instance, SerializationInfo Info)
		{
			foreach (dynamic each in GetTarget(Instance))
				if (each is FieldInfo)
					Info.AddValue(each.Name, each.GetValue(Instance), each.FieldType);
				else if (each is PropertyInfo)
					Info.AddValue(each.Name, each.GetValue(Instance), each.PropertyType);
		}

		public static void Deserialize(object Instance, SerializationInfo Info)
		{
			foreach (dynamic each in GetTarget(Instance))
				if (each is FieldInfo)
					each.SetValue(Instance, Info.GetValue(each.Name, each.FieldType));
				else if (each is PropertyInfo)
					each.SetValue(Instance, Info.GetValue(each.Name, each.PropertyType));
		}

		public static byte[] Serialize(object Instance)
		{
			MemoryStream Stream = new MemoryStream();

			BF.Serialize(Stream, Instance);
			byte[] Buffer = Stream.GetBuffer();
			Stream.Close();

			return Buffer;
		}

		public static object Deserialize(byte[] Data)
		{
			MemoryStream Stream = new MemoryStream(Data);

			object Instance = BF.Deserialize(Stream);
			Stream.Close();

			return Instance;
		}
		#endregion

		#region Helper
		static IEnumerable<MemberInfo> GetTarget(object Instance)
		{
			return from q in Instance.GetType().GetMembers(BindingFlags.GetField | BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
				   where q.GetCustomAttribute<SerializationTargetAttribute>() != null
				   select q;
		}
		#endregion
	}
}