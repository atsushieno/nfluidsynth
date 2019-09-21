using System;

namespace NFluidsynth
{
	public abstract class FluidsynthObject : IDisposable
	{
		IntPtr handle;
		bool dispose_required;

		protected FluidsynthObject (IntPtr handle, bool disposeRequired)
		{
			this.handle = handle;
			dispose_required = disposeRequired;
		}

		internal IntPtr Handle {
			get { return handle; }
		}

		protected abstract void OnDispose ();

		public void Dispose ()
		{
			if (dispose_required)
				OnDispose ();
		}

		public override int GetHashCode ()
		{
			return (int) handle;
		}

		public override bool Equals (object obj)
		{
			return Equals (obj as FluidsynthObject);
		}

		protected virtual bool Equals (FluidsynthObject obj)
		{
			return (object) obj != null && handle == obj.handle;
		}
	}
}

