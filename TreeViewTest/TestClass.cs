using System;
using System.Collections.Generic;

namespace TreeViewTest
{
	public abstract class BaseClass
	{
		#region Field
		protected static Random R = new Random();
		#endregion

		#region Property
		public int Value
		{
			get
			{
				return R.Next();
			}
		}
		#endregion
	}

	public class TestClass_1 : BaseClass
	{
		#region Constructor
		public TestClass_1()
		{
			Son = new TestClass_2();

			Sons = new List<TestClass_2>();
			int Length = R.Next(1, 10);
			for (int i = 0; i < Length; i++)
				((List<TestClass_2>)Sons).Add(new TestClass_2());
		}
		#endregion

		#region Property
		public TestClass_2 Son
		{
			private set;
			get;
		}

		public IEnumerable<TestClass_2> Sons
		{
			private set;
			get;
		}
		#endregion
	}

	public class TestClass_2 : BaseClass
	{
	}
}