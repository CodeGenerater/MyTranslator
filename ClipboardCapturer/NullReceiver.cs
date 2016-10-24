using System;
using System.Threading;

namespace CodeGenerater.Translation.Plugins
{
	class NullReceiver : Receiver
	{
		#region Constructor
		public NullReceiver()
		{
			ReceiverResetEvent.Set();
			ThreadResetEvent.Set();
		}
		#endregion

		#region Method
		public override void Receive(object Data)
		{

		}

		public override object Request()
		{
			ThreadResetEvent.WaitOne();
			return null;
		}

		protected override object Load()
		{
			return null;
		}

		protected override void Save(object Data)
		{

		}

		public void Stop()
		{
			ThreadResetEvent.Reset();
		}

		public void Start()
		{
			ThreadResetEvent.Set();
		}
		#endregion
	}
}