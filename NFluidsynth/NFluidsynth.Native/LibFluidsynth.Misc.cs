using System;
using System.Runtime.InteropServices;

namespace NFluidsynth.Native
{
	static partial class LibFluidsynth
	{
		internal static class Misc
		{
			[DllImport (LibraryName)]
			[return:MarshalAs (UnmanagedType.SysInt)]
			internal extern static bool fluid_is_soundfont (string filename);

			[DllImport (LibraryName)]
			[return:MarshalAs (UnmanagedType.SysInt)]
			internal extern static bool fluid_is_midifile (string filename);

			[DllImport (LibraryName)]
			internal extern static IntPtr fluid_synth_new_stream_sfloader (IntPtr synth, IntPtr stream);

			[DllImport (LibraryName)]
			internal extern static void fluid_synth_delete_stream_sfloader (IntPtr loader);

#if ANDROID
			[DllImport (LibraryName)]
			internal extern static IntPtr new_fluid_android_asset_stream_loader (IntPtr jniEnv, IntPtr assetManagerJObject);

			[DllImport (LibraryName)]
			internal extern static void free_fluid_android_asset_stream_loader (IntPtr handle);
#endif
		}
	}
}
