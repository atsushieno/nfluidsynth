using System;
using NFluidsynth.Native;

namespace NFluidsynth
{
	public class SoundFontLoader : FluidsynthObject
	{
		public SoundFontLoader (Synth synth, StreamLoader streamLoader)
			: base (LibFluidsynth.Misc.fluid_synth_new_stream_sfloader (synth.Handle, streamLoader.Handle), true)
		{
		}

		public SoundFontLoader (IntPtr handle)
			: base (handle, false)
		{
		}

		protected override void OnDispose ()
		{
			LibFluidsynth.Misc.fluid_synth_delete_stream_sfloader (Handle);
		}
	}
}

