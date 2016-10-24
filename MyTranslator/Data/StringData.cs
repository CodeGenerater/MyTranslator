using System;
using System.Collections.Generic;

namespace CodeGenerater.Translation
{
	public class StringData : IStringData
	{
		#region Constructor
		public StringData(string Original)
		{
			this.Original = Original;
		}
		#endregion

		#region Field
		Stack<string> _StringStack = new Stack<string>();
		#endregion

		#region Property
		public string Current
		{
			set
			{
				_StringStack.Push(value);
			}
			get
			{
				return _StringStack.Peek();
			}
		}

		public string Original
		{
			private set;
			get;
		}

		public Stack<string> StringStack
		{
			get
			{
				return new Stack<string>(_StringStack);
			}
		}
		#endregion

		#region Method
		public void Register(string Data)
		{
			_StringStack.Push(Data);
		}
		#endregion
	}
}