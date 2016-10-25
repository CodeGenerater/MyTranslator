namespace System.Threading
{
	public abstract class Receiver : IDisposable
	{
		#region Constructor
		public Receiver()
		{
			ReceiverResetEvent = new AutoResetEvent(true);
			ThreadResetEvent = new AutoResetEvent(false);
			CanReceive = true;
		}
		#endregion

		#region Finalizer
		~Receiver()
		{
			Dispose();
		}
		#endregion
		
		#region Property
		protected bool CanReceive
		{
			set;
			get;
		}

		protected AutoResetEvent ReceiverResetEvent
		{
			private set;
			get;
		}

		protected AutoResetEvent ThreadResetEvent
		{
			private set;
			get;
		}
		#endregion

		#region Method
		public virtual void Receive(object Data)
		{
			if (!CanReceive)
				ReceiverResetEvent.Reset();

			ReceiverResetEvent.WaitOne();
			Save(Data);
			ThreadResetEvent.Set();
		}

		public virtual object Request()
		{
			ThreadResetEvent.WaitOne();
			object Data = Load();
			ReceiverResetEvent.Set();
			return Data;
		}

		public virtual void Dispose()
		{
			if (ReceiverResetEvent != null)
			{
				ReceiverResetEvent.Dispose();
				ReceiverResetEvent = null;
			}

			if (ThreadResetEvent != null)
			{
				ThreadResetEvent.Dispose();
				ThreadResetEvent = null;
			}
		}

		protected abstract void Save(object Data);

		protected abstract object Load();
		#endregion
	}
}