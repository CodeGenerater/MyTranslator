using System;
using System.Threading;

namespace CodeGenerater.Translation.Plugins
{
	public class EzTrans : Plugin
	{
		#region Field
		const string EZTR_INIT_STR = "CSUSER123455";

		J2KEngine Engine;
		string _Path;

		EncodingManager CP932;
		EncodingManager CP949;
		#endregion

		#region Property
		[SerializationTarget]
		public string Path
		{
			set
			{
				if (_Path != value)
				{
					_Path = value;
					Notify("Path");
				}
			}
			get
			{
				return _Path;
			}
		}
		#endregion

		#region Helper
		protected override void Initialize()
		{
			CP932 = new EncodingManager(932);
			CP949 = new EncodingManager(949);
			Engine = new J2KEngine(Path);
			Engine.J2K_InitializeEx(CP932.ToBytes(EZTR_INIT_STR), CP932.ToBytes(Path + @"\Dat"));

			ProcessFunction = (object Input) =>
			{
				StringData Data = Input as StringData;

				if (Data == null)
					throw new ArgumentNullException();

				Data.Register(Translate(Data.Current));

				return Data;
			};
		}

		string Translate(string Input)
		{
			if (string.IsNullOrWhiteSpace(Input))
				throw new RequireSkipException();

			byte[] input = CP932.ToBytes(Input);
			IntPtr Pointer = Engine.J2K_TranslateMMNT(0, input);
			string output = CP949.ToString(Pointer);
			Engine.J2K_FreeMem(Pointer);
			return output;
		}
		#endregion
	}
}