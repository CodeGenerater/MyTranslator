using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace CodeGenerater.Translation
{
	public static class AssemblyExtensions
	{
		public static IEnumerable<Type> GetPlugins(this Assembly Assembly)
		{
			return from q in Assembly.DefinedTypes
				   where q.IsInherite(typeof(Plugin))
				   select q;
		}
	}
}