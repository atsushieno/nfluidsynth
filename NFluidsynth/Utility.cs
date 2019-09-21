using NFluidsynth.Native;

namespace NFluidsynth
{
    internal static class Utility
    {
        public static void CheckReturnValue(int value)
        {
            if (value != LibFluidsynth.FluidOk)
            {
                throw new FluidSynthInteropException();
            }
        }
    }
}
