using System;
using System.Reflection;

namespace CodeGenerater.Translation
{
	public class PluginType
	{
		#region Constructor
		public PluginType(Type T)
		{
			if (T == null)
				throw new ArgumentNullException("T");

			if (!T.IsInherite(typeof(Plugin)))
				throw new ArgumentException();

			Type = T;

			if (GetConstructor() == null)
				throw new ArgumentException();
		}
		#endregion

		#region Property
		public Type Type
		{
			private set;
			get;
		}

		public string Name
		{
			get
			{
				return Type?.Name;
			}
		}

		public string Namespace
		{
			get
			{
				return Type?.Namespace;
			}
		}
		#endregion

		#region Method
		public Plugin Create()
		{
			return (Plugin)GetConstructor().Invoke(null);
		}
		#endregion

		#region Helper
		ConstructorInfo GetConstructor()
		{
			return Type.GetConstructor(Type.EmptyTypes);
		}
		#endregion
	}
}