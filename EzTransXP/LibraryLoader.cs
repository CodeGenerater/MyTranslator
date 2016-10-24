using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CodeGenerater.Translation.Plugins
{
	public class LibraryLoader : IDisposable
	{
		#region DllImport
		[DllImport("kernel32.dll")]
		static extern IntPtr LoadLibrary(string dllToLoad);

		[DllImport("kernel32.dll")]
		static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

		[DllImport("kernel32.dll")]
		static extern bool FreeLibrary(IntPtr hModule);
		#endregion

		#region Constructor
		static LibraryLoader()
		{
			Default = new LibraryLoader();
		}
		#endregion

		#region Field
		Dictionary<string, IntPtr> ModuleDict = new Dictionary<string, IntPtr>();
		#endregion

		#region Property
		public static LibraryLoader Default { private set; get; }
		#endregion

		#region Method
		public TDelegate Load<TDelegate>(string DllName, string ProcedureName)
		{
			return Marshal.GetDelegateForFunctionPointer<TDelegate>(GetProcAddress(GetModule(DllName), ProcedureName));
		}

		public object Load(string DllName, string ProcedureName, Type DelegateType)
		{
			return Marshal.GetDelegateForFunctionPointer(GetProcAddress(GetModule(DllName), ProcedureName), DelegateType);
		}

		public void Unload(string DllName)
		{
			if (ModuleDict.ContainsKey(DllName))
				FreeLibrary(ModuleDict[DllName]);
			else
				throw new ArgumentException();
		}
		#endregion

		#region Helper
		IntPtr GetModule(string DllName)
		{
			if (ModuleDict.ContainsKey(DllName))
				return ModuleDict[DllName];
			else
			{
				IntPtr Module = LoadLibrary(DllName);

				if (Module == IntPtr.Zero)
					throw new ArgumentException();

				ModuleDict.Add(DllName, Module);

				return Module;
			}
		}
		#endregion

		#region IDisposable
		public void Dispose()
		{
			foreach (var each in ModuleDict.Values)
				FreeLibrary(each);
		}
		#endregion
	}
}