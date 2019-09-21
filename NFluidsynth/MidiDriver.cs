using System;
using NFluidsynth.Native;

namespace NFluidsynth
{
	public class MidiDriver : FluidsynthObject
	{
		public unsafe MidiDriver (Settings settings, MidiEventHandler handler)
			: base (LibFluidsynth.new_fluid_midi_driver (settings.Handle, (d, e) => handler (d, new MidiEvent (e)), null), true)
		{
		}

		protected internal MidiDriver (IntPtr handle)
			: base (handle, false)
		{
		}

		protected override void OnDispose ()
		{
			LibFluidsynth.delete_fluid_midi_driver (Handle);
		}
	}
}
