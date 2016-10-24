using Microsoft.Win32;

namespace CodeGenerater.Translation.Plugins
{
	public static class EzTransPathSearcher
	{
		#region Field
		const string REG_SUBKEY_NAME = @"Software\ChangShin\ezTrans";
		const string REG_VALUE_NAME = "FilePath";

		static string _Path;
		#endregion

		#region Property
		public static string Path
		{
			get
			{
				if (_Path == null)
					_Path = Search();
				return _Path;
			}
		}
		#endregion

		#region Method
		public static string Search()
		{
			return Registry.CurrentUser.OpenSubKey(REG_SUBKEY_NAME).GetValue(REG_VALUE_NAME).ToString();
		}

		public static string Search(string SubKeyName, string ValueName)
		{
			return Registry.CurrentUser.OpenSubKey(SubKeyName).GetValue(ValueName).ToString();
		}
		#endregion
	}
}