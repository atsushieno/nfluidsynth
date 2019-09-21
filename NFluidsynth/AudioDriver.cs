using System;
using System.Runtime.InteropServices;
using NFluidsynth.Native;

namespace NFluidsynth
{
    public class AudioDriver : FluidsynthObject
    {
        public delegate int AudioHandler(float[][] outBuffer);

        public AudioDriver(Settings settings, Synth synth)
            : base(LibFluidsynth.new_fluid_audio_driver(settings.Handle, synth.Handle), true)
        {
        }

        protected override void OnDispose()
        {
            LibFluidsynth.delete_fluid_audio_driver(Handle);
        }
    }
}
