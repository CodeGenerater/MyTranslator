using System;

namespace CodeGenerater.Translation
{
	public static class TypeExtensions
	{
		public static bool IsInherite(this Type Target, Type Base)
		{
			Type T = Target;
			while(true)
			{
				if (T.BaseType == Base)
					return true;
				else if (T.BaseType == typeof(object))
					return false;

				T = T.BaseType;
			}
		}
	}
}