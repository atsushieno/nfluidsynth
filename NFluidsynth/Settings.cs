using System;
using NFluidsynth.Native;

namespace NFluidsynth
{
	public class Settings : FluidsynthObject
	{
		public Settings ()
			: base (LibFluidsynth.new_fluid_settings (), true)
		{
		}

		protected internal Settings (IntPtr handle)
			: base (handle, false)
		{
		}

		protected override void OnDispose ()
		{
			LibFluidsynth.delete_fluid_settings (Handle);
		}

		public SettingEntry this [string name] {
			get { return new SettingEntry (Handle, name); }
		}

		public class SettingEntry
		{
			internal SettingEntry (IntPtr handle, string name)
			{
				this.handle = handle;
				this.name = name;
			}

			readonly IntPtr handle;
			readonly string name;

			public string Name {
				get { return name; }
			}

			public FluidTypes Type {
				get { return LibFluidsynth.fluid_settings_get_type (handle, name); }
			}

			public FluidHint Hints {
				get { return LibFluidsynth.fluid_settings_get_hints (handle, name); }
			}

			public string StringDefalut {
				get { return LibFluidsynth.fluid_settings_getstr_default (handle, name); }
			}

			public string StringValue {
				get {
					string v;
					LibFluidsynth.fluid_settings_getstr (handle, name, out v);
					return v;
				}
				set {
					LibFluidsynth.fluid_settings_setstr (handle, name, value);
				}
			}

			public string IntDefault {
				get { return LibFluidsynth.fluid_settings_getstr_default (handle, name); }
			}

			public Tuple<int, int> GetIntRange ()
			{
				int min, max;
				LibFluidsynth.fluid_settings_getint_range (handle, name, out min, out max); 
				return new Tuple<int, int> (min, max);
			}

			public int IntValue {
				get {
					int v;
					LibFluidsynth.fluid_settings_getint (handle, name, out v);
					return v;
				}
				set {
					LibFluidsynth.fluid_settings_setint (handle, name, value);
				}
			}

			public double DoubleDefault {
				get { return LibFluidsynth.fluid_settings_getnum_default (handle, name); }
			}

			public Tuple<double, double> GetDoubleRange ()
			{
				double min, max;
				LibFluidsynth.fluid_settings_getnum_range (handle, name, out min, out max); 
				return new Tuple<double, double> (min, max);
			}

			public double DoubleValue {
				get {
					double v;
					LibFluidsynth.fluid_settings_getnum (handle, name, out v);
					return v;
				}
				set {
					LibFluidsynth.fluid_settings_setnum (handle, name, value);
				}
			}

			public void ForeachOption (Action<string,string> func)
			{
				LibFluidsynth.fluid_settings_foreach_option_t f = (d, nm, opt) => { func (nm, opt); return IntPtr.Zero; };
				LibFluidsynth.fluid_settings_foreach_option (handle, name, IntPtr.Zero, f);
			}

			public void ForeachOption (Func<IntPtr,string,string,IntPtr> func, IntPtr data = default (IntPtr))
			{
				LibFluidsynth.fluid_settings_foreach_option_t f = (d, nm, opt) => func (d, nm, opt);
				LibFluidsynth.fluid_settings_foreach_option (handle, name, data, f);
			}

			public void Foreach (Action<string,FluidTypes> func)
			{
				LibFluidsynth.fluid_settings_foreach_t f = (d, nm, t) => { func (nm, t); return IntPtr.Zero; };
				LibFluidsynth.fluid_settings_foreach (handle, name, IntPtr.Zero, f);
			}

			public void Foreach (Func<IntPtr,string,FluidTypes,IntPtr> func, IntPtr data = default (IntPtr))
			{
				LibFluidsynth.fluid_settings_foreach_t f = (d, nm, t) => func (d, nm, t);
				LibFluidsynth.fluid_settings_foreach (handle, name, data, f);
			}
		}
	}
}

