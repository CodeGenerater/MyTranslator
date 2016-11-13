using System;
using System.Threading;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace CodeGenerater.Translation
{
	[Serializable]
	public abstract class Plugin : SingleCapacityPipe, ISerializable, INotifyPropertyChanged
	{
		#region ISerializable
		protected virtual void Serialize(SerializationInfo Info, StreamingContext Context)
		{
			SerializationHelper.Serialize(this, Info);
		}

		public virtual void GetObjectData(SerializationInfo Info, StreamingContext Context)
		{
			Serialize(Info, Context);
		}

		protected virtual void Deserialize(SerializationInfo Info, StreamingContext Context)
		{
			SerializationHelper.Deserialize(this, Info);
			Initialize();
		}

		Plugin(SerializationInfo Info, StreamingContext Context)
		{
			Deserialize(Info, Context);
		}
		#endregion

		#region Constructor
		public Plugin()
		{
			Initialize();
		}
		#endregion

		#region INotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;

		protected void Notify(string PropertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
		}
		#endregion

		#region Field
		string _Name;

		string _Version;

		string _Creator;
		#endregion
		
		#region Property
		[SerializationTarget]
		public string Name
		{
			set
			{
				if (_Name != value)
				{
					_Name = value;
					Notify("Name");
				}
			}
			get
			{
				return _Name;
			}
		}

		[SerializationTarget]
		public string Version
		{
			set
			{
				if (_Version != value)
				{
					_Version = value;
					Notify("Version");
				}
			}
			get
			{
				return _Version;
			}
		}

		[SerializationTarget]
		public string Creator
		{
			set
			{
				if (_Creator != value)
				{
					_Creator = value;
					Notify("Creator");
				}
			}
			get
			{
				return _Creator;
			}
		}
		#endregion

		#region Method
		public override string ToString()
		{
			return string.Format("{0} ({1}) by {2}", Name, Version, Creator);
		}
		#endregion

		#region Helper
		public override void Initialize() { }
		#endregion
	}
}