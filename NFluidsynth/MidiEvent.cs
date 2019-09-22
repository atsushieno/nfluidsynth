using System;
using NFluidsynth.Native;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;

namespace NFluidsynth
{
    public class MidiEvent : FluidsynthObject
    {
        private readonly bool _owned;

        public MidiEvent()
            : base(LibFluidsynth.new_fluid_midi_event())
        {
            _owned = true;
        }

        protected internal MidiEvent(IntPtr handle)
            : base(handle)
        {
            _owned = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (_owned && !Disposed)
            {
                LibFluidsynth.delete_fluid_midi_event(Handle);
            }
        }

        public int Type
        {
            get { return LibFluidsynth.fluid_midi_event_get_type(Handle); }
            set { LibFluidsynth.fluid_midi_event_set_type(Handle, value); }
        }

        public int Channel
        {
            get { return LibFluidsynth.fluid_midi_event_get_channel(Handle); }
            set { LibFluidsynth.fluid_midi_event_set_channel(Handle, value); }
        }

        public int Key
        {
            get { return LibFluidsynth.fluid_midi_event_get_key(Handle); }
            set { LibFluidsynth.fluid_midi_event_set_key(Handle, value); }
        }

        public int Velocity
        {
            get { return LibFluidsynth.fluid_midi_event_get_velocity(Handle); }
            set { LibFluidsynth.fluid_midi_event_set_velocity(Handle, value); }
        }

        public int Control
        {
            get { return LibFluidsynth.fluid_midi_event_get_control(Handle); }
            set { LibFluidsynth.fluid_midi_event_set_control(Handle, value); }
        }

        public int Value
        {
            get { return LibFluidsynth.fluid_midi_event_get_value(Handle); }
            set { LibFluidsynth.fluid_midi_event_set_value(Handle, value); }
        }

        public int Program
        {
            get { return LibFluidsynth.fluid_midi_event_get_program(Handle); }
            set { LibFluidsynth.fluid_midi_event_set_program(Handle, value); }
        }

        public int Pitch
        {
            get { return LibFluidsynth.fluid_midi_event_get_pitch(Handle); }
            set { LibFluidsynth.fluid_midi_event_set_pitch(Handle, value); }
        }

        public unsafe void SetSysex(void* data, int size, bool isDynamic)
        {
            LibFluidsynth.fluid_midi_event_set_sysex(Handle, data, size, isDynamic);
        }
    }
}