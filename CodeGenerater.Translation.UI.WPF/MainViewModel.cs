using System;

namespace CodeGenerater.Translation.UI.WPF
{
	class MainViewModel
	{
		#region Constructor
		public MainViewModel()
		{
			MyTranslator = MyTranslator.Load();
		}
		#endregion

		#region Property
		public MyTranslator MyTranslator
		{
			private set;
			get;
		}
		#endregion
	}
}