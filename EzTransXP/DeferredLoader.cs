using System;

namespace CodeGenerater.Translation.Plugins
{
	public class DeferredLoader<TLoadable>
	{
		#region Constructor
		public DeferredLoader() { }
		#endregion

		#region Field
		TLoadable _Loaded;
		#endregion

		#region Event
		public event EventHandler Loading;
		#endregion

		#region Property
		public TLoadable Loaded
		{
			set
			{
				if (value == null) throw new ArgumentNullException();

				if (!ReferenceEquals(_Loaded, value)) _Loaded = value;
			}
			get
			{
				if (_Loaded == null) Loading(this, null);

				return _Loaded;
			}
		}
		#endregion
	}
}