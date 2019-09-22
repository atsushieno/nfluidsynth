using System;
using System.Runtime.InteropServices;
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

        public static IntPtr PassDelegatePointer<T>(T input, out T output) where T : Delegate
        {
            output = input;
            return Marshal.GetFunctionPointerForDelegate(input);
        }
    }
}
