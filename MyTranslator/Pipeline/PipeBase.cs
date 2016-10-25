namespace System.Threading
{
	public abstract class PipeBase : IDisposable
	{
		#region Finalizer
		~PipeBase()
		{
			Dispose();
		}
		#endregion

		#region Property
		public Receiver Receiver
		{
			protected set;
			get;
		}

		public Receiver NextReceiver
		{
			internal set;
			get;
		}
		#endregion

		#region Method
		public virtual void Dispose() { }
		#endregion
	}
}