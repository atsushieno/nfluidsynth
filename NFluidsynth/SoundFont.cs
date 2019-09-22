using System;
using NFluidsynth.Native;

namespace NFluidsynth
{
    public class SoundFont : FluidsynthObject
    {
        public static bool IsSoundFont(string filename)
        {
            return LibFluidsynth.fluid_is_soundfont(filename);
        }

        public static bool IsMidiFile(string filename)
        {
            return LibFluidsynth.fluid_is_midifile(filename);
        }

        protected internal SoundFont(IntPtr handle)
            : base(handle)
        {
        }
    }
}