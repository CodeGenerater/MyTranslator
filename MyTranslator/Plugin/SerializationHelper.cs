using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CodeGenerater.Translation
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
	public sealed class SerializationTargetAttribute : Attribute
	{

	}

	public static class SerializationHelper
	{
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

		static IEnumerable<MemberInfo> GetTarget(object Instance)
		{
			return from q in Instance.GetType().GetMembers(BindingFlags.GetField | BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
				   where q.GetCustomAttribute<SerializationTargetAttribute>() != null
				   select q;
		}
	}
}