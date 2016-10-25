namespace System.Threading
{
	public class SingleReceiver : Receiver
	{
		#region Field
		object Data;
		#endregion

		#region Method
		protected override object Load()
		{
			object Output = Data;
			Data = null;
			CanReceive = true;
			return Output;
		}

		protected override void Save(object Data)
		{
			CanReceive = false;
			this.Data = Data;
		}
		#endregion
	}
}