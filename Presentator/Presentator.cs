using System;

namespace CodeGenerater.Translation.Plugins
{
	public class Presentator : Plugin
	{
		#region Field
		string _OriginalText;

		string _TranslatedText;
		#endregion

		#region Property
		public string OriginalText
		{
			private set
			{
				if (_OriginalText != value)
				{
					_OriginalText = value;
					Notify("OriginalText");
				}
			}
			get
			{
				return _OriginalText;
			}
		}

		public string TranslatedText
		{
			private set
			{
				if (_TranslatedText != value)
				{
					_TranslatedText = value;
					Notify("TranslatedText");
				}
			}
			get
			{
				return _TranslatedText;
			}
		}
		#endregion

		#region Method
		public override object Process(object Data)
		{
			var StringData = Data as StringData;

			string Original = StringData.Original;
			string Current = StringData.Current;

			App.Current.Dispatcher.BeginInvoke(
				new Action(() =>
				{
					OriginalText = Original;
					TranslatedText = Current;
				}));

			return null;
		}
		#endregion

		#region Helper
		public override void Initialize()
		{
			var MainWindow = new MainWindow();
			MainWindow.DataContext = this;
			MainWindow.Show();
		}
		#endregion
	}
}