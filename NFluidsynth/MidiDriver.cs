using System;
using NFluidsynth.Native;

namespace NFluidsynth
{
	public class MidiDriver : FluidsynthObject
	{
		public MidiDriver (Settings settings, MidiEventHandler handler, byte [] handlerData)
			: base (LibFluidsynth.new_fluid_midi_driver (settings.Handle, (d, e) => handler (d, new MidiEvent (e)), handlerData), true)
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
