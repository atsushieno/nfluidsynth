using System;

namespace NFluidsynth
{
    public class SoundFont : FluidsynthObject
    {
        protected internal SoundFont(IntPtr handle)
            : base(handle)
        {
        }
    }
}