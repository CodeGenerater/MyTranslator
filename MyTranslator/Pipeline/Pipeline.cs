using System.Linq;
using System.Collections.Generic;

namespace System.Threading
{
	public class Pipeline : PipeBase
	{
		#region Constructor
		public Pipeline(IEnumerable<PipeBase> PipeCollection)
		{
			if (PipeCollection == null)
				throw new ArgumentNullException("PipeCollection");

			this.PipeCollection = PipeCollection.ToArray();
			this.PipeCollection.Aggregate((PipeBase A, PipeBase B) => { A.NextReceiver = B.Receiver; return B; });

			Receiver = this.PipeCollection.First().Receiver;
			NextReceiver = this.PipeCollection.Last().NextReceiver;
		}
		#endregion

		#region Field
		IEnumerable<PipeBase> PipeCollection;
		#endregion

		#region Method
		public override void Dispose()
		{
			if (PipeCollection != null)
			{
				foreach (var each in PipeCollection)
					each.Dispose();
				PipeCollection = null;
			}
		}

		public void Run(object Data)
		{
			Receiver.Receive(Data);
		}
		#endregion
	}
}