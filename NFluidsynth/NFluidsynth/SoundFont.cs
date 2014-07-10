using System;
using NFluidsynth.Native;
using System.Runtime.InteropServices;

namespace NFluidsynth
{
	public class SoundFont : FluidsynthObject
	{
		protected internal SoundFont (IntPtr handle)
			: base (handle, false)
		{
		}

		protected override void OnDispose ()
		{
			// do nothing
		}
	}
}

