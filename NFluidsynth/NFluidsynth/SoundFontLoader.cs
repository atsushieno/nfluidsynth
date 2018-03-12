using System;
using NFluidsynth.Native;
using static NFluidsynth.Native.LibFluidsynth;

namespace NFluidsynth
{
	public abstract class SoundFontLoader : IDisposable
	{
		public SoundFontLoader (Synth synth)
		{
			i = new SoundFontLoaderInternal (synth, (p, f) => Load (f), p => Free ());
		}

		SoundFontLoaderInternal i;

		public void Dispose ()
		{
			if (i != null)
				i.Dispose ();
			i = null;
		}

		internal IntPtr Handle {
			get { return i.Handle; }
		}

		public abstract IntPtr Load (string filename);

		public abstract void Free ();

		public abstract IntPtr Open (string filename);
		public abstract int Read (IntPtr buf, long count, IntPtr handle);
		public abstract int Seek (IntPtr handle, long position, int origin); // I avoid System.IO-specific enum here.
		public abstract int Tell (IntPtr handle);
		public abstract int Close (IntPtr handle);
	}
	
	class SoundFontLoaderInternal : FluidsynthObject
	{
		public SoundFontLoaderInternal (Synth synth, SfLoader.fluid_sfloader_load_t load, SfLoader.fluid_sfloader_free_t free)
			: base (SfLoader.new_fluid_sfloader (load, free), true)
		{
		}

		public SoundFontLoaderInternal (IntPtr handle)
			: base (handle, false)
		{
		}

		protected override void OnDispose ()
		{
			SfLoader.delete_fluid_sfloader (Handle);
		}

		public void SetCallbacks (SoundFontLoader loader)
		{
			SfLoader.fluid_sfloader_set_callbacks (Handle, loader.Open, (b, l, p) => loader.Read (b, l, p), (p, i, o) => loader.Seek (p, i, o), p => loader.Tell (p), p => loader.Close (p));
		}
	}
}

