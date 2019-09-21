using System;
using NFluidsynth.Native;
using System.Runtime.InteropServices;
using System.Linq;

namespace NFluidsynth
{
	public class AudioDriver : FluidsynthObject
	{
		public delegate int AudioHandler (byte [] data, float [][] outBuffer);

		public AudioDriver (Settings settings, Synth synth)
			: base (LibFluidsynth.new_fluid_audio_driver (settings.Handle, synth.Handle), true)
		{
		}

		public AudioDriver (Settings settings, AudioHandler handler, byte[] data)
			: base (LibFluidsynth.new_fluid_audio_driver2 (settings.Handle, (dt, len, nin, inBuffer, nout, outBuffer) => {
				try {
					var bufPtrs = new IntPtr [nout];
					Marshal.Copy (outBuffer, bufPtrs, 0, nout);
					var buf = new float [nout] [];
					for (int i = 0; i < nout; i++)
						Marshal.Copy (bufPtrs [i], buf [i], 0, len);
					handler (dt, buf);
					return 0;
				} catch (Exception) {
					return -1;
				}
				}, data), true)
		{
		}

		protected override void OnDispose ()
		{
			LibFluidsynth.delete_fluid_audio_driver (Handle);
		}
	}
}

