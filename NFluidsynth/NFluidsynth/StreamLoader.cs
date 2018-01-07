using System;
using NFluidsynth.Native;

namespace NFluidsynth
{
	public class StreamLoader : FluidsynthObject
	{
		protected internal StreamLoader (IntPtr handle)
			: base (handle, false)
		{
		}

		protected override void OnDispose ()
		{
		}
	}

#if ANDROID
	public class AndroidAssetStreamLoader : StreamLoader
	{
		public AndroidAssetStreamLoader (Android.Content.Res.AssetManager assetManager)
			: base (LibFluidsynth.Misc.new_fluid_android_asset_stream_loader (Android.Runtime.JNIEnv.Handle, assetManager.Handle))
		{
		}

		protected override void OnDispose ()
		{
			LibFluidsynth.Misc.free_fluid_android_asset_stream_loader (Handle);
		}
	}
#endif
}
