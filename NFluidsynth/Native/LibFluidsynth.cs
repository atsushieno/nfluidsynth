using System;
using System.Runtime.InteropServices;

namespace NFluidsynth.Native
{
    internal static partial class LibFluidsynth
    {
        public const string LibraryName = "fluidsynth";

        public const int FluidOk = 0;
        public const int FluidFailed = -1;

#if NETCOREAPP
        static LibFluidsynth()
        {
            NativeLibrary.SetDllImportResolver(typeof(LibFluidsynth).Assembly, (name, assembly, path) =>
            {
                if (name == LibraryName)
                {
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    {
                        return NativeLibrary.Load("libfluidsynth.so");
                    }

                    if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    {
                        return NativeLibrary.Load("libfluidsynth.dylib");
                    }
                }

                return IntPtr.Zero;
            });
        }
#endif
    }
}
