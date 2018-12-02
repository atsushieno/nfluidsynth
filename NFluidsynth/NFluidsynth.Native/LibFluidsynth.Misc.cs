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
			internal extern static IntPtr fluid_set_log_function (int severity, Logger.LoggerDelegate func, IntPtr data);
		}
	}
}
