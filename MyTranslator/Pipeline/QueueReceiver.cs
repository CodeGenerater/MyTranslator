using System.Collections.Concurrent;

namespace System.Threading
{
	public class QueueReceiver : Receiver
	{
		#region Field
		ConcurrentQueue<object> DataQueue = new ConcurrentQueue<object>();
		#endregion

		#region Method
		protected override object Load()
		{
			object Data;
			while (true)
			{
				ThreadResetEvent.WaitOne();
				if (DataQueue.TryDequeue(out Data))
					return Data;
				else if(DataQueue.Count == 0)
					ThreadResetEvent.Reset();
			}
		}

		protected override void Save(object Data)
		{
			DataQueue.Enqueue(Data);
		}
		#endregion
	}
}