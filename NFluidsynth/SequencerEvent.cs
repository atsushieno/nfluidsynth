using System;
using NFluidsynth.Native;

namespace NFluidsynth
{
    public class SequencerEvent : FluidsynthObject
    {
        private readonly bool _owned;

        public SequencerEvent()
            : base(LibFluidsynth.new_fluid_event())
        {
            _owned = true;
        }

        protected internal SequencerEvent(IntPtr handle)
            : base(handle)
        {
            _owned = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (_owned && !Disposed)
            {
                LibFluidsynth.delete_fluid_event(Handle);
            }
        }

        public FluidSequencerEventType Type
        {
            get
            {
                ThrowIfDisposed();
                return (FluidSequencerEventType) LibFluidsynth.fluid_event_get_type(Handle);
            }
        }

        public SequencerClientId Source
        {
            get
            {
                ThrowIfDisposed();
                return new SequencerClientId(LibFluidsynth.fluid_event_get_source(Handle));
            }
            set
            {
                ThrowIfDisposed();
                LibFluidsynth.fluid_event_set_source(Handle, value.Value);
            }
        }

        public SequencerClientId Dest
        {
            get
            {
                ThrowIfDisposed();
                return new SequencerClientId(LibFluidsynth.fluid_event_get_dest(Handle));
            }
            set
            {
                ThrowIfDisposed();
                LibFluidsynth.fluid_event_set_dest(Handle, value.Value);
            }
        }

        public int Channel
        {
            get
            {
                ThrowIfDisposed();
                return LibFluidsynth.fluid_event_get_channel(Handle);
            }
        }

        public short Key
        {
            get
            {
                ThrowIfDisposed();
                return LibFluidsynth.fluid_event_get_key(Handle);
            }
        }

        public short Velocity
        {
            get
            {
                ThrowIfDisposed();
                return LibFluidsynth.fluid_event_get_velocity(Handle);
            }
        }

        public short Control
        {
            get
            {
                ThrowIfDisposed();
                return LibFluidsynth.fluid_event_get_control(Handle);
            }
        }

        public short Value
        {
            get
            {
                ThrowIfDisposed();
                return LibFluidsynth.fluid_event_get_value(Handle);
            }
        }

        public short Program
        {
            get
            {
                ThrowIfDisposed();
                return LibFluidsynth.fluid_event_get_program(Handle);
            }
        }

        public unsafe void* Data
        {
            get
            {
                ThrowIfDisposed();
                return LibFluidsynth.fluid_event_get_data(Handle);
            }
        }

        public uint Duration
        {
            get
            {
                ThrowIfDisposed();
                return LibFluidsynth.fluid_event_get_duration(Handle);
            }
        }

        public short Bank
        {
            get
            {
                ThrowIfDisposed();
                return LibFluidsynth.fluid_event_get_bank(Handle);
            }
        }

        public int Pitch
        {
            get
            {
                ThrowIfDisposed();
                return LibFluidsynth.fluid_event_get_pitch(Handle);
            }
        }

        public uint SoundFontId
        {
            get
            {
                ThrowIfDisposed();
                return LibFluidsynth.fluid_event_get_sfont_id(Handle);
            }
        }

        public unsafe void Timer(void* data)
        {
            ThrowIfDisposed();
            LibFluidsynth.fluid_event_timer(Handle, data);
        }

        public void Note(int channel, short key, short vel, uint duration)
        {
            ThrowIfDisposed();
            LibFluidsynth.fluid_event_note(Handle, channel, key, vel, duration);
        }

        public void NoteOn(int channel, short key, short vel)
        {
            ThrowIfDisposed();
            LibFluidsynth.fluid_event_noteon(Handle, channel, key, vel);
        }

        public void NoteOff(int channel, short key)
        {
            ThrowIfDisposed();
            LibFluidsynth.fluid_event_noteoff(Handle, channel, key);
        }

        public void AllSoundsOff(int channel)
        {
            ThrowIfDisposed();
            LibFluidsynth.fluid_event_all_sounds_off(Handle, channel);
        }

        public void AllNotesOff(int channel)
        {
            ThrowIfDisposed();
            LibFluidsynth.fluid_event_all_notes_off(Handle, channel);
        }

        public void BankSelect(int channel, short bankNum)
        {
            ThrowIfDisposed();
            LibFluidsynth.fluid_event_bank_select(Handle, channel, bankNum);
        }

        public void ProgramChange(int channel, short val)
        {
            ThrowIfDisposed();
            LibFluidsynth.fluid_event_program_change(Handle, channel, val);
        }

        public void ProgramSelect(int channel, uint soundFontId, short bankNum, short presetNum)
        {
            ThrowIfDisposed();
            LibFluidsynth.fluid_event_program_select(Handle, channel, soundFontId, bankNum, presetNum);
        }

        public void ControlChange(int channel, short control, short val)
        {
            ThrowIfDisposed();
            LibFluidsynth.fluid_event_control_change(Handle, channel, control, val);
        }

        public void PitchBend(int channel, int pitch)
        {
            ThrowIfDisposed();
            LibFluidsynth.fluid_event_pitch_bend(Handle, channel, pitch);
        }

        public void PitchWheelSensitivity(int channel, short value)
        {
            ThrowIfDisposed();
            LibFluidsynth.fluid_event_pitch_wheelsens(Handle, channel, value);
        }

        public void Modulation(int channel, short val)
        {
            ThrowIfDisposed();
            LibFluidsynth.fluid_event_modulation(Handle, channel, val);
        }

        public void Sustain(int channel, short val)
        {
            ThrowIfDisposed();
            LibFluidsynth.fluid_event_sustain(Handle, channel, val);
        }

        public void Pan(int channel, short val)
        {
            ThrowIfDisposed();
            LibFluidsynth.fluid_event_pan(Handle, channel, val);
        }

        public void Volume(int channel, short val)
        {
            ThrowIfDisposed();
            LibFluidsynth.fluid_event_volume(Handle, channel, val);
        }

        public void ReverbSend(int channel, short val)
        {
            ThrowIfDisposed();
            LibFluidsynth.fluid_event_reverb_send(Handle, channel, val);
        }

        public void ChorusSend(int channel, short val)
        {
            ThrowIfDisposed();
            LibFluidsynth.fluid_event_chorus_send(Handle, channel, val);
        }

        public void KeyPressure(int channel, short key, short val)
        {
            ThrowIfDisposed();
            LibFluidsynth.fluid_event_key_pressure(Handle, channel, key, val);
        }

        public void ChannelPressure(int channel, short val)
        {
            ThrowIfDisposed();
            LibFluidsynth.fluid_event_channel_pressure(Handle, channel, val);
        }

        public void SystemReset()
        {
            ThrowIfDisposed();
            LibFluidsynth.fluid_event_system_reset(Handle);
        }

        public void AnyControlChange(int channel)
        {
            ThrowIfDisposed();
            LibFluidsynth.fluid_event_any_control_change(Handle, channel);
        }

        public void Unregistering()
        {
            ThrowIfDisposed();
            LibFluidsynth.fluid_event_unregistering(Handle);
        }
    }
}