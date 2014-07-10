using System;
using System.Runtime.InteropServices;

using fluid_settings_t_ptr = System.IntPtr;

namespace NFluidsynth.Native
{
	static partial class LibFluidsynth
	{
		internal delegate IntPtr fluid_settings_foreach_option_t (IntPtr data, string name, string option);
		internal delegate IntPtr fluid_settings_foreach_t (IntPtr data, string name, [MarshalAs (UnmanagedType.SysInt)] FluidTypes type);

		internal static partial class Settings
		{
			[DllImport (LibraryName)]
			internal static extern fluid_settings_t_ptr new_fluid_settings ();

			[DllImport (LibraryName)]
			internal static extern void delete_fluid_settings (fluid_settings_t_ptr settings);

			[DllImport (LibraryName)]
			internal static extern FluidTypes fluid_settings_get_type (fluid_settings_t_ptr settings, string name);

			[DllImport (LibraryName)]
			internal static extern FluidHint fluid_settings_get_hints (fluid_settings_t_ptr settings, string name);

			[DllImport (LibraryName)]
			internal static extern bool fluid_settings_is_realtime (fluid_settings_t_ptr settings, string name);

			[DllImport (LibraryName)]
			internal static extern void fluid_settings_setstr (fluid_settings_t_ptr settings, string name, string str);

			[DllImport (LibraryName)]
			internal static extern void fluid_settings_copystr (fluid_settings_t_ptr settings, string name, IntPtr str, [MarshalAs (UnmanagedType.SysInt)] int len);

			// There is no chance to release the returned pointer, so do not bind it.
			//[DllImport (LibraryName)]
			//internal static extern int fluid_settings_dupstr (fluid_settings_t_ptr settings, string name, out string str);

			[DllImport (LibraryName)]
			internal static extern bool fluid_settings_getstr (fluid_settings_t_ptr settings, string name, out string str);

			[DllImport (LibraryName)]
			internal static extern string fluid_settings_getstr_default (fluid_settings_t_ptr settings, string name);

			[DllImport (LibraryName)]
			internal static extern bool fluid_settings_str_equal (fluid_settings_t_ptr settings, string name, string s);

			[DllImport (LibraryName)]
			internal static extern void fluid_settings_setnum (fluid_settings_t_ptr settings, string name, double val);

			[return:MarshalAs (UnmanagedType.SysInt)]
			[DllImport (LibraryName)]
			internal static extern int fluid_settings_getnum (fluid_settings_t_ptr settings, string name, out double val);

			[DllImport (LibraryName)]
			internal static extern double fluid_settings_getnum_default (fluid_settings_t_ptr settings, string name);

			[DllImport (LibraryName)]
			internal static extern void fluid_settings_getnum_range (fluid_settings_t_ptr settings, string name, out double min, out double max);

			[DllImport (LibraryName)]
			internal static extern void fluid_settings_setint (fluid_settings_t_ptr settings, string name, [MarshalAs (UnmanagedType.SysInt)] int val);

			[DllImport (LibraryName)]
			internal static extern bool fluid_settings_getint (fluid_settings_t_ptr settings, string name, [MarshalAs (UnmanagedType.SysInt)] out int val);

			[return:MarshalAs (UnmanagedType.SysInt)]
			[DllImport (LibraryName)]
			internal static extern int fluid_settings_getint_default (fluid_settings_t_ptr settings, string name);

			[DllImport (LibraryName)]
			internal static extern void fluid_settings_getint_range (fluid_settings_t_ptr settings, string name, [MarshalAs (UnmanagedType.SysInt)] out int min, [MarshalAs (UnmanagedType.SysInt)] out int max);

			[DllImport (LibraryName)]
			internal static extern void fluid_settings_foreach_option (fluid_settings_t_ptr settings, string name, IntPtr data, fluid_settings_foreach_option_t func);

			[return:MarshalAs (UnmanagedType.SysInt)]
			[DllImport (LibraryName)]
			internal static extern int fluid_settings_option_count (fluid_settings_t_ptr settings, string name);

			[DllImport (LibraryName)] // note: the returned value has to be released.
			internal static extern IntPtr fluid_settings_option_concat (fluid_settings_t_ptr settings, string name, string separator);

			[DllImport (LibraryName)]
			internal static extern void fluid_settings_foreach (fluid_settings_t_ptr settings, string name, IntPtr data, fluid_settings_foreach_t func);
		}
	}
}
