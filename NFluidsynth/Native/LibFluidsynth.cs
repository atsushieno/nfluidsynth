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
            try
            {
                NativeLibrary.SetDllImportResolver(typeof(LibFluidsynth).Assembly, (name, assembly, path) =>
                {
                    IntPtr handle;
                    if (name == LibraryName)
                    {
                        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                        {
                            // Assumption here is that this binds against whatever API .2 is,
                            //  but will try the general name anyway just in case.
                            if (NativeLibrary.TryLoad("libfluidsynth.so.2", assembly, path, out handle))
                                return handle;

                            if (NativeLibrary.TryLoad("libfluidsynth.so", assembly, path, out handle))
                                return handle;
                        }

                        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                        {
                            if (NativeLibrary.TryLoad("libfluidsynth.dylib", assembly, path, out handle))
                                return handle;
                        }
                    }

                    return IntPtr.Zero;
                });
            }
            catch (Exception)
            {
                // An exception can be thrown in the above call if someone has already set a DllImportResolver.
                // (Can occur if the application wants to override behaviour.)
                // This does not throw away failures to resolve.
            }
        }
#endif
    }
}
