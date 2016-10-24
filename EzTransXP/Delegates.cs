using System;
using System.Runtime.InteropServices;

namespace CodeGenerater.Translation.Plugins
{
	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	public delegate void FreeMemDelegate(IntPtr Address);

	[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
	public delegate bool InitExDelegate(byte[] InitStr, byte[] HomeDir);

	[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
	public delegate IntPtr TranslationDelegate(int data0, byte[] Japanese);

	[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode)]
	public delegate IntPtr TranslationDelegate2Byte(int data0, string Japanese);
}