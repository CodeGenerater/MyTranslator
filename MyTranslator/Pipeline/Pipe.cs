namespace System.Threading
{
	public class Pipe : PipeBase
	{
		#region Constructor
		public Pipe()
		{
			Thread = new Thread(ThreadStart);
			Thread.Start();
		}
		#endregion

		#region Field
		Thread Thread;

		bool Continue = true;
		#endregion

		#region Property
		public Func<object, object> ProcessFunction
		{
			protected set;
			get;
		}
		#endregion

		#region Method
		public override void Dispose()
		{
			Continue = false;
			if (Thread != null)
			{
				Thread.Abort();
				Thread = null;
			}
		}
		#endregion

		#region Helper
		void ThreadStart()
		{
			while (Continue)
			{
				object Data = Receiver.Request();

				if (ProcessFunction == null)
					throw new NullReferenceException();

				try { NextReceiver?.Receive(ProcessFunction(Data)); }
				catch (RequireSkipException) { }
			}
		}
		#endregion
	}
}