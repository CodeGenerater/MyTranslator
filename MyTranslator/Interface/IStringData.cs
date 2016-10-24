using System.Collections.Generic;

namespace CodeGenerater.Translation
{
	public interface IStringData
	{
		string Original
		{
			get;
		}

		Stack<string> StringStack
		{
			get;
		}

		string Current
		{
			get;
		}
	}
}
