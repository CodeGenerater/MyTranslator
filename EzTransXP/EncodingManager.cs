using System;
using System.Text;
using System.Collections.Generic;

namespace CodeGenerater.Translation.Plugins
{
	public class EncodingManager
	{
		#region Constructor
		public EncodingManager(Encoding BaseEncoding)
		{
			this.BaseEncoding = BaseEncoding;
		}

		public EncodingManager(int CodePage)
			: this(Encoding.GetEncoding(CodePage))
		{
		}
		#endregion

		#region Property
		public Encoding BaseEncoding { private set; get; }
		#endregion

		#region Method
		public byte[] ToBytes(string Input)
		{
			return BaseEncoding.GetBytes(Input);
		}

		public byte[] ToBytes(IntPtr Input)
		{
			return PointerToArray(Input);
		}

		public string ToString(byte[] Input)
		{
			return BaseEncoding.GetString(Input);
		}

		public string ToString(IntPtr Input)
		{
			return ToString(PointerToArray(Input));
		}

		public IntPtr ToPointer(byte[] Input)
		{
			return ArrayToPointer(Input);
		}

		public IntPtr ToPointer(string Input)
		{
			return ArrayToPointer(BaseEncoding.GetBytes(Input));
		}
		#endregion

		#region Helper
		IntPtr ArrayToPointer(byte[] Array)
		{
			unsafe
			{
				fixed(byte* ptr = Array)
					return (IntPtr)ptr;
			}
		}

		unsafe byte[]  PointerToArray(IntPtr Pointer)
		{
			byte* ptr = (byte*)Pointer.ToPointer();
			List<byte> List = new List<byte>();

			for (int i = 0; ptr[i] != 0; i++) List.Add(ptr[i]);

			return List.ToArray();
		}
		#endregion
	}
}