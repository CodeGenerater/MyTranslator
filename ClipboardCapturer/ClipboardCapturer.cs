using System.Windows;
using System.Threading;
using System.Runtime.Serialization;

namespace CodeGenerater.Translation.Plugins
{
	public class ClipboardCapturer : Plugin
	{
		#region Serialization
		protected override void Deserialize(SerializationInfo Info, StreamingContext Context)
		{
			base.Deserialize(Info, Context);
		}
		#endregion

		#region Constructor
		public ClipboardCapturer() : base(ReceiverType.None)
		{
			
		}
		#endregion

		#region Field
		string _Captured;

		int _Interval;
		#endregion
		
		#region Method
		public string Captured
		{
			private set
			{
				if (_Captured != value)
				{
					_Captured = value;
					Notify("Captured");
				}
			}
			get
			{
				return _Captured;
			}
		}

		[SerializationTarget]
		public int Interval
		{
			set
			{
				if (_Interval != value)
				{
					_Interval = value;
					Notify("Interval");
				}
			}
			get
			{
				return _Interval;
			}
		}
		#endregion

		#region Helper
		protected override void Initialize()
		{
			Receiver = new NullReceiver();

			ProcessFunction = (object @null) =>
			{
				string Capture = Clipboard.GetText();

				if (Captured != Capture)
					return Captured = Capture;

				Thread.Sleep(Interval);
				throw new RequireSkipException();
			};
		}
		#endregion
	}
}