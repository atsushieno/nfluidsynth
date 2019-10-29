using System;
using System.IO;
using static NFluidsynth.Native.LibFluidsynth;

namespace NFluidsynth
{
    public delegate SoundFont SoundFontLoaderLoadDelegate(SoundFontLoader loader, string filename);

    public delegate SoundFont SoundFontLoaderFreeDelegate(SoundFontLoader loader);

    public abstract class SoundFontLoaderCallbacks
    {
        // used as fluid_sfloader_callback_open_t
        public abstract IntPtr Open(string filename);

        internal unsafe IntPtr Open(byte* data) => Open(Utility.PtrToStringUTF8(data));

        // used as fluid_sfloader_callback_read_t
        public abstract int Read(IntPtr buf, long count, IntPtr sfHandle);

        public int Read(IntPtr buf, int count, IntPtr sfHandle) => Read(buf, (long) count, sfHandle);

        // used as fluid_sfloader_callback_seek_t
        public abstract int Seek(IntPtr sfHandle, int offset, SeekOrigin origin);

        public int Seek(IntPtr sfHandle, int offset, int origin) => Seek(sfHandle, offset, (SeekOrigin) origin);

        // used as fluid_sfloader_callback_tell_t
        public abstract int Tell(IntPtr sfHandle);

        // used as fluid_sfloader_callback_close_t
        public abstract int Close(IntPtr sfHandle);
    }

    public class SoundFontLoader : IDisposable
    {
        IntPtr handle; // fluid_sfloader_t*

        // We keep these around so the GC doesn't eat them.
        private fluid_sfloader_callback_open_t _open;
        private fluid_sfloader_callback_read_t _read;
        private fluid_sfloader_callback_seek_t _seek;
        private fluid_sfloader_callback_tell_t _tell;
        private fluid_sfloader_callback_close_t _close;

        public static SoundFontLoader NewDefaultSoundFontLoader(Settings settings)
        {
            return new SoundFontLoader(new_fluid_defsfloader(settings.Handle));
        }

        public SoundFontLoader(SoundFontLoaderLoadDelegate load, SoundFontLoaderFreeDelegate free)
            : this(new_fluid_sfloader(
                (loaderHandle, filename) => load(new SoundFontLoader(loaderHandle), filename).Handle,
                loaderHandle => free(new SoundFontLoader(loaderHandle))))
        {
        }

        protected SoundFontLoader(IntPtr handle)
        {
            this.handle = handle;
        }

        public unsafe void SetCallbacks(SoundFontLoaderCallbacks callbacks)
        {
            fluid_sfloader_set_callbacks(handle,
                Utility.PassDelegatePointer(callbacks.Open, out _open),
                Utility.PassDelegatePointer(callbacks.Read, out _read),
                Utility.PassDelegatePointer(callbacks.Seek, out _seek),
                Utility.PassDelegatePointer(callbacks.Tell, out _tell),
                Utility.PassDelegatePointer(callbacks.Close, out _close));
        }

        public virtual void Dispose()
        {
            if (handle != IntPtr.Zero)
                delete_fluid_sfloader(handle);
            handle = IntPtr.Zero;
        }

        // It has to be made public for use in the derived loader classes
        public IntPtr Handle
        {
            get { return handle; }
        }

        public IntPtr Data
        {
            get { return fluid_sfloader_get_data(handle); }
            set { fluid_sfloader_set_data(handle, value); }
        }
    }
}
