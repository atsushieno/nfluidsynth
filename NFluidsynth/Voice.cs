using System;
using NFluidsynth.Native;
using System.Runtime.InteropServices;
using System.Text;

namespace NFluidsynth
{
	public class Voice : FluidsynthObject
	{
		protected internal Voice (IntPtr handle)
			: base (handle, false)
		{
		}

		protected override void OnDispose ()
		{
			throw new NotImplementedException ();
		}
	}
}

