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

        public FluidSequencerEventType Type => (FluidSequencerEventType) LibFluidsynth.fluid_event_get_type(Handle);

        public SequencerClientId Source
        {
            get => new SequencerClientId(LibFluidsynth.fluid_event_get_source(Handle));
            set => LibFluidsynth.fluid_event_set_source(Handle, value.Value);
        }

        public SequencerClientId Dest
        {
            get => new SequencerClientId(LibFluidsynth.fluid_event_get_dest(Handle));
            set => LibFluidsynth.fluid_event_set_dest(Handle, value.Value);
        }

        public int Channel => LibFluidsynth.fluid_event_get_channel(Handle);
        public short Key => LibFluidsynth.fluid_event_get_key(Handle);
        public short Velocity => LibFluidsynth.fluid_event_get_velocity(Handle);
        public short Control => LibFluidsynth.fluid_event_get_control(Handle);
        public short Value => LibFluidsynth.fluid_event_get_value(Handle);
        public short Program => LibFluidsynth.fluid_event_get_program(Handle);
        public unsafe void* Data => LibFluidsynth.fluid_event_get_data(Handle);
        public uint Duration => LibFluidsynth.fluid_event_get_duration(Handle);
        public short Bank => LibFluidsynth.fluid_event_get_bank(Handle);
        public int Pitch => LibFluidsynth.fluid_event_get_pitch(Handle);
        public uint SoundFontId => LibFluidsynth.fluid_event_get_sfont_id(Handle);

        public unsafe void Timer(void* data)
        {
            LibFluidsynth.fluid_event_timer(Handle, data);
        }

        public void Note(int channel, short key, short vel, uint duration)
        {
            LibFluidsynth.fluid_event_note(Handle, channel, key, vel, duration);
        }

        public void NoteOn(int channel, short key, short vel)
        {
            LibFluidsynth.fluid_event_noteon(Handle, channel, key, vel);
        }

        public void NoteOff(int channel, short key)
        {
            LibFluidsynth.fluid_event_noteoff(Handle, channel, key);
        }

        public void AllSoundsOff(int channel)
        {
            LibFluidsynth.fluid_event_all_sounds_off(Handle, channel);
        }

        public void AllNotesOff(int channel)
        {
            LibFluidsynth.fluid_event_all_notes_off(Handle, channel);
        }

        public void BankSelect(int channel, short bankNum)
        {
            LibFluidsynth.fluid_event_bank_select(Handle, channel, bankNum);
        }

        public void ProgramChange(int channel, short val)
        {
            LibFluidsynth.fluid_event_program_change(Handle, channel, val);
        }

        public void ProgramSelect(int channel, uint soundFontId, short bankNum, short presetNum)
        {
            LibFluidsynth.fluid_event_program_select(Handle, channel, soundFontId, bankNum, presetNum);
        }

        public void ControlChange(int channel, short control, short val)
        {
            LibFluidsynth.fluid_event_control_change(Handle, channel, control, val);
        }

        public void PitchBend(int channel, int pitch)
        {
            LibFluidsynth.fluid_event_pitch_bend(Handle, channel, pitch);
        }

        public void PitchWheelSensitivity(int channel, short value)
        {
            LibFluidsynth.fluid_event_pitch_wheelsens(Handle, channel, value);
        }

        public void Modulation(int channel, short val)
        {
            LibFluidsynth.fluid_event_modulation(Handle, channel, val);
        }

        public void Sustain(int channel, short val)
        {
            LibFluidsynth.fluid_event_sustain(Handle, channel, val);
        }

        public void Pan(int channel, short val)
        {
            LibFluidsynth.fluid_event_pan(Handle, channel, val);
        }

        public void Volume(int channel, short val)
        {
            LibFluidsynth.fluid_event_volume(Handle, channel, val);
        }

        public void ReverbSend(int channel, short val)
        {
            LibFluidsynth.fluid_event_reverb_send(Handle, channel, val);
        }

        public void ChorusSend(int channel, short val)
        {
            LibFluidsynth.fluid_event_chorus_send(Handle, channel, val);
        }

        public void KeyPressure(int channel, short key, short val)
        {
            LibFluidsynth.fluid_event_key_pressure(Handle, channel, key, val);
        }

        public void ChannelPressure(int channel, short val)
        {
            LibFluidsynth.fluid_event_channel_pressure(Handle, channel, val);
        }

        public void SystemReset()
        {
            LibFluidsynth.fluid_event_system_reset(Handle);
        }

        public void AnyControlChange(int channel)
        {
            LibFluidsynth.fluid_event_any_control_change(Handle, channel);
        }

        public void Unregistering()
        {
            LibFluidsynth.fluid_event_unregistering(Handle);
        }
    }
}