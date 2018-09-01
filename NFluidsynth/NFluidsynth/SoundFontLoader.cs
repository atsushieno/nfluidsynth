using System;
using System.IO;
using NFluidsynth.Native;
using static NFluidsynth.Native.LibFluidsynth;

namespace NFluidsynth
{
	public delegate SoundFont SoundFontLoaderLoadDelegate (SoundFontLoader loader, string filename);
	public delegate SoundFont SoundFontLoaderFreeDelegate (SoundFontLoader loader);

	public abstract class SoundFontLoaderCallbacks
	{
		// used as fluid_sfloader_callback_open_t
		public abstract IntPtr Open (string filename);
		// used as fluid_sfloader_callback_read_t
		public abstract int Read (IntPtr buf, long count, IntPtr sfHandle);
		// used as fluid_sfloader_callback_seek_t
		public abstract int Seek (IntPtr sfHandle, int offset, SeekOrigin origin);
		// used as fluid_sfloader_callback_tell_t
		public abstract int Tell (IntPtr sfHandle);
		// used as fluid_sfloader_callback_close_t
		public abstract int Close (IntPtr sfHandle);
	}

	public class SoundFontLoader : IDisposable
	{
		IntPtr handle; // fluid_sfloader_t*

		public static SoundFontLoader NewDefaultSoundFontLoader (Settings settings)
		{
			return new SoundFontLoader (SfLoader.new_fluid_defsfloader (settings.Handle));
		}

		public SoundFontLoader (SoundFontLoaderLoadDelegate load, SoundFontLoaderFreeDelegate free)
			: this (SfLoader.new_fluid_sfloader ((loaderHandle, filename) => load (new SoundFontLoader (loaderHandle), filename).Handle, (loaderHandle) => free (new SoundFontLoader (loaderHandle))))
		{
		}

		protected SoundFontLoader (IntPtr handle)
		{
			this.handle = handle;
		}

		protected void SetCallbacks (SoundFontLoaderCallbacks callbacks)
		{
			SfLoader.fluid_sfloader_set_callbacks (handle,
			                                       f => callbacks.Open (f),
			                                       (b, l, h) => callbacks.Read (b, l, h), 
			                                       (h, p, i) => callbacks.Seek (h, p, (SeekOrigin) i),
			                                       h => callbacks.Tell (h),
			                                       h => callbacks.Close (h));
		}

		public void Dispose ()
		{
			if (handle != IntPtr.Zero)
				SfLoader.delete_fluid_sfloader (handle);
			handle = IntPtr.Zero;
		}

		internal IntPtr Handle {
			get { return handle; }
		}

		public IntPtr Data {
			get { return SfLoader.fluid_sfloader_get_data (handle); }
			set { SfLoader.fluid_sfloader_set_data (handle, value); }
		}
	}
}

