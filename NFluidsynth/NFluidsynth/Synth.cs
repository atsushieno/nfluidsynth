using System;
using NFluidsynth.Native;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;

namespace NFluidsynth
{
	[StructLayout (LayoutKind.Sequential)]
	public struct FluidChannelInfo
	{
		[MarshalAs (UnmanagedType.SysInt)]
		public int Assigned;
		[MarshalAs (UnmanagedType.SysInt)]
		public int SoundFontId;
		[MarshalAs (UnmanagedType.SysInt)]
		public int Bank;
		[MarshalAs (UnmanagedType.SysInt)]
		public int Program;
		[MarshalAs (UnmanagedType.ByValTStr, SizeConst = 32)]
		public string Name;
		[MarshalAs (UnmanagedType.ByValArray, SizeConst = 32)]
		public byte [] Reserved;
	}

	public class Synth : FluidsynthObject
	{
		public Synth (Settings settings)
			: base (LibFluidsynth.Synth.new_fluid_synth (settings.Handle), true)
		{
		}

		protected internal Synth (IntPtr handle)
			: base (handle, false)
		{
		}

		protected override void OnDispose ()
		{
			LibFluidsynth.Synth.delete_fluid_synth (Handle);
		}

		public Settings Settings {
			get { return new Settings (LibFluidsynth.Synth.fluid_synth_get_settings (Handle)); }
		}

		public void NoteOn (int channel, int key, int vel)
		{
			if (LibFluidsynth.Synth.fluid_synth_noteon (Handle, channel, key, vel) != 0)
				throw new FluidSynthInteropException ("noteon operation failed " + LastError);
		}

		public void NoteOff (int channel, int key)
		{
			// not sure if we should always raise exception, it seems that it also returns FUILD_FAILED for not-on-state note.
			if (LibFluidsynth.Synth.fluid_synth_noteoff (Handle, channel, key) != 0)
				throw new FluidSynthInteropException ("noteoff operation failed " + LastError);
		}

		public void CC (int channel, int num, int val)
		{
			if (LibFluidsynth.Synth.fluid_synth_cc (Handle, channel, num, val) != 0)
				throw new FluidSynthInteropException ("control change operation failed " + LastError);
		}

		public int GetCC (int channel, int num)
		{
			int ret;
			if (LibFluidsynth.Synth.fluid_synth_get_cc (Handle, channel, num, out ret) != 0)
				throw new FluidSynthInteropException ("control change get operation failed " + LastError);
			return ret;
		}

		public bool Sysex (byte [] input, byte [] output, bool dryrun = false)
		{
			int outlen = output != null ? output.Length : 0;
			bool handled;
			if (LibFluidsynth.Synth.fluid_synth_sysex (Handle, input, input.Length, output, ref outlen, out handled, dryrun) != 0)
				throw new FluidSynthInteropException ("sysex operation failed " + LastError);
			return handled;
		}

		public void PitchBend (int channel, int val)
		{
			if (LibFluidsynth.Synth.fluid_synth_pitch_bend (Handle, channel, val) != 0)
				throw new FluidSynthInteropException ("pitch bend change operation failed " + LastError);
		}

		public int GetPitchBend (int channel)
		{
			int ret;
			if (LibFluidsynth.Synth.fluid_synth_get_pitch_bend (Handle, channel, out ret) != 0)
				throw new FluidSynthInteropException ("pitch bend get operation failed " + LastError);
			return ret;
		}

		public void PitchWheelSens (int channel, int val)
		{
			if (LibFluidsynth.Synth.fluid_synth_pitch_wheel_sens (Handle, channel, val) != 0)
				throw new FluidSynthInteropException ("pitch wheel sens change operation failed " + LastError);
		}

		public int GetPitchWheelSens (int channel)
		{
			int ret;
			if (LibFluidsynth.Synth.fluid_synth_get_pitch_wheel_sens (Handle, channel, out ret) != 0)
				throw new FluidSynthInteropException ("pitch wheel sens get operation failed " + LastError);
			return ret;
		}

		public void ProgramChange (int channel, int program)
		{
			if (LibFluidsynth.Synth.fluid_synth_program_change (Handle, channel, program) != 0)
				throw new FluidSynthInteropException ("program change operation failed " + LastError);
		}

		public void ChannelPressure (int channel, int val)
		{
			if (LibFluidsynth.Synth.fluid_synth_channel_pressure (Handle, channel, val) != 0)
				throw new FluidSynthInteropException ("channel pressure change operation failed " + LastError);
		}

		public void BankSelect (int channel, uint bank)
		{
			if (LibFluidsynth.Synth.fluid_synth_bank_select (Handle, channel, bank) != 0)
				throw new FluidSynthInteropException ("bank select operation failed " + LastError);
		}

		public void SoundFontSelect (int channel, uint soundFontId)
		{
			if (LibFluidsynth.Synth.fluid_synth_sfont_select (Handle, channel, soundFontId) != 0)
				throw new FluidSynthInteropException ("sound font select operation failed " + LastError);
		}

		public void ProgramSelect (int channel, uint soundFontId, uint bank, uint preset)
		{
			if (LibFluidsynth.Synth.fluid_synth_program_select (Handle, channel, soundFontId, bank, preset) != 0)
				throw new FluidSynthInteropException ("program select operation failed " + LastError);
		}

		public void ProgramSelectBySoundFontName (int channel, string soundFontName, uint bank, uint preset)
		{
			if (LibFluidsynth.Synth.fluid_synth_program_select_by_sfont_name (Handle, channel, soundFontName, bank, preset) != 0)
				throw new FluidSynthInteropException ("program select (by sound font name) operation failed " + LastError);
		}

		public void GetProgram (int channel, out int soundFontId, out int bank, out int preset)
		{
			if (LibFluidsynth.Synth.fluid_synth_get_program (Handle, channel, out soundFontId, out bank, out preset) != 0)
				throw new FluidSynthInteropException ("program get operation failed " + LastError);
		}

		public void UnsetProgram (int channel)
		{
			if (LibFluidsynth.Synth.fluid_synth_unset_program (Handle, channel) != 0)
				throw new FluidSynthInteropException ("program unset operation failed " + LastError);
		}

		public IntPtr GetChannelInfo (int channel)
		{
			IntPtr info;
			if (LibFluidsynth.Synth.fluid_synth_get_channel_info (Handle, channel, out info) != 0)
				throw new FluidSynthInteropException ("channel info get operation failed " + LastError);
			return info;
		}

		public void ProgramReset ()
		{
			if (LibFluidsynth.Synth.fluid_synth_program_reset (Handle) != 0)
				throw new FluidSynthInteropException ("program reset operation failed " + LastError);
		}

		public void SystemReset ()
		{
			if (LibFluidsynth.Synth.fluid_synth_system_reset (Handle) != 0)
				throw new FluidSynthInteropException ("system reset operation failed " + LastError);
		}

		// fluid_synth_get_channel_preset() is deprecated, so I don't bind it.
		// Then fluid_synth_start() takes fluid_preset_t* which is returned only by this deprecated function, so I don't bind it either.
		// Then fluid_synth_stop() is paired by the function above, so I don't bind it either.

		public void LoadSoundFont (string filename, bool resetPresets)
		{
			if (LibFluidsynth.Synth.fluid_synth_sfload (Handle, filename, resetPresets) < 0)
				throw new FluidSynthInteropException ("sound font load operation failed " + LastError);
		}

		public void ReloadSoundFont (uint id)
		{
			if (LibFluidsynth.Synth.fluid_synth_sfreload (Handle, id) != 0)
				throw new FluidSynthInteropException ("sound font reload operation failed " + LastError);
		}

		public void UnloadSoundFont (uint id, bool resetPresets)
		{
			if (LibFluidsynth.Synth.fluid_synth_sfunload (Handle, id, resetPresets) != 0)
				throw new FluidSynthInteropException ("sound font unload operation failed " + LastError);
		}

		public void AddSoundFont (SoundFont soundFont)
		{
			if (LibFluidsynth.Synth.fluid_synth_add_sfont (Handle, soundFont.Handle) != 0)
				throw new FluidSynthInteropException ("sound font add operation failed " + LastError);
		}

		public void RemoveSoundFont (SoundFont soundFont)
		{
			LibFluidsynth.Synth.fluid_synth_remove_sfont (Handle, soundFont.Handle);
		}

		public int FontCount {
			get { return LibFluidsynth.Synth.fluid_synth_sfcount (Handle); }
		}

		public SoundFont GetSoundFont (uint index)
		{
			IntPtr ret = LibFluidsynth.Synth.fluid_synth_get_sfont (Handle, index);
			return ret == IntPtr.Zero ? null : new SoundFont (ret);
		}

		public SoundFont GetSoundFontById (uint id)
		{
			IntPtr ret = LibFluidsynth.Synth.fluid_synth_get_sfont_by_id (Handle, id);
			return ret == IntPtr.Zero ? null : new SoundFont (ret);
		}

		public SoundFont GetSoundFontByName (string name)
		{
			IntPtr ret = LibFluidsynth.Synth.fluid_synth_get_sfont_by_name (Handle, name);
			return ret == IntPtr.Zero ? null : new SoundFont (ret);
		}

		public void SetBankOffset (int soundFontId, int offset)
		{
			if (LibFluidsynth.Synth.fluid_synth_set_bank_offset (Handle, soundFontId, offset) != 0)
				throw new FluidSynthInteropException ("bank offset set operation failed " + LastError);
		}

		public void GetBankOffset (int soundFontId)
		{
			LibFluidsynth.Synth.fluid_synth_get_bank_offset (Handle, soundFontId);
		}

		public void SetReverb (double roomSize, double damping, double width, double level)
		{
			LibFluidsynth.Synth.fluid_synth_set_reverb (Handle, roomSize, damping, width, level);
		}

		public void SetReverbOn (bool enabled)
		{
			LibFluidsynth.Synth.fluid_synth_set_reverb_on (Handle, enabled);
		}

		public double ReverbRoomSize {
			get { return LibFluidsynth.Synth.fluid_synth_get_reverb_roomsize (Handle); }
		}

		public double ReverbDamp {
			get { return LibFluidsynth.Synth.fluid_synth_get_reverb_damp (Handle); }
		}

		public double ReverbLevel {
			get { return LibFluidsynth.Synth.fluid_synth_get_reverb_level (Handle); }
		}

		public double ReverbWidth {
			get { return LibFluidsynth.Synth.fluid_synth_get_reverb_width (Handle); }
		}

		public void SetChorus (int numVoices, double level, double speed, double depthMS, FluidChorusMod type)
		{
			LibFluidsynth.Synth.fluid_synth_set_chorus (Handle, numVoices, level, speed, depthMS, type);
		}

		public void SetChorusOn (bool enabled)
		{
			LibFluidsynth.Synth.fluid_synth_set_chorus_on (Handle, enabled);
		}

		public int NumberOfChorusVoices {
			get { return LibFluidsynth.Synth.fluid_synth_get_chorus_nr (Handle); }
		}

		public double ChorusLevel {
			get { return LibFluidsynth.Synth.fluid_synth_get_chorus_level (Handle); }
		}

		public double ChorusSpeedHz {
			get { return LibFluidsynth.Synth.fluid_synth_get_chorus_speed_Hz (Handle); }
		}

		public double ChorusDepthMS {
			get { return LibFluidsynth.Synth.fluid_synth_get_chorus_depth_ms (Handle); }
		}

		public FluidChorusMod ChorusType {
			get { return LibFluidsynth.Synth.fluid_synth_get_chorus_type (Handle); }
		}

		public int MidiChannelCount {
			get { return LibFluidsynth.Synth.fluid_synth_count_midi_channels (Handle); }
		}

		public int AudioChannelCount {
			get { return LibFluidsynth.Synth.fluid_synth_count_audio_channels (Handle); }
		}

		public int AudioGroupCount {
			get { return LibFluidsynth.Synth.fluid_synth_count_audio_groups (Handle); }
		}

		public int EffectChannelCount {
			get { return LibFluidsynth.Synth.fluid_synth_count_effects_channels (Handle); }
		}

		public void SetChannelRate (float sampleRate)
		{
			LibFluidsynth.Synth.fluid_synth_set_sample_rate (Handle, sampleRate);
		}

		public float Gain {
			get { return LibFluidsynth.Synth.fluid_synth_get_gain (Handle); }
			set { LibFluidsynth.Synth.fluid_synth_set_gain (Handle, value); }
		}

		public int Polyphony {
			get { return LibFluidsynth.Synth.fluid_synth_get_polyphony (Handle); }
			set { LibFluidsynth.Synth.fluid_synth_set_polyphony (Handle, value); }
		}

		public int ActiveVoiceCount {
			get { return LibFluidsynth.Synth.fluid_synth_get_active_voice_count (Handle); }
		}

		public int InternalBufferSize {
			get { return LibFluidsynth.Synth.fluid_synth_get_internal_bufsize (Handle); }
		}

		public void SetInterpolationMethod (int channel, FluidInterpolation interpolationMethod)
		{
			if (LibFluidsynth.Synth.fluid_synth_set_interp_method (Handle, channel, interpolationMethod) != 0)
				throw new FluidSynthInteropException ("interpolation method set operation failed " + LastError);
		}

		public void SetGenerator (int channel, int param, float value)
		{
			if (LibFluidsynth.Synth.fluid_synth_set_gen (Handle, channel, param, value) != 0)
				throw new FluidSynthInteropException ("generator set operation failed " + LastError);
		}

		public void SetGenerator (int channel, int param, float value, bool absolute, bool normalized)
		{
			if (LibFluidsynth.Synth.fluid_synth_set_gen2 (Handle, channel, param, value, absolute, normalized) != 0)
				throw new FluidSynthInteropException ("generator set2 operation failed " + LastError);
		}

		public float GetGenerator (int channel, int param)
		{
			return LibFluidsynth.Synth.fluid_synth_get_gen (Handle, channel, param);
		}

		#region Tuning

		public void CreateKeyTuning (int bank, int prog, string name, double [] pitch)
		{
			if (pitch == null)
				throw new ArgumentNullException ("pitch");
			if (pitch.Length != 128)
				throw new ArgumentException ("pitch array must be of 128 elements.");
			if (LibFluidsynth.Synth.fluid_synth_create_key_tuning (Handle, bank, prog, name, pitch) != 0)
				throw new FluidSynthInteropException ("key tuning create operation failed " + LastError);
		}

		public void ActivateKeyTuning (int bank, int prog, string name, double [] pitch, bool apply)
		{
			if (pitch == null)
				throw new ArgumentNullException ("pitch");
			if (pitch.Length != 128)
				throw new ArgumentException ("pitch array must be of 128 elements.");
			if (LibFluidsynth.Synth.fluid_synth_activate_key_tuning (Handle, bank, prog, name, pitch, apply) != 0)
				throw new FluidSynthInteropException ("key tuning create operation failed " + LastError);
		}

		public void CreateOctaveTuning (int bank, int prog, string name, double [] pitch)
		{
			if (pitch == null)
				throw new ArgumentNullException ("pitch");
			if (pitch.Length != 128)
				throw new ArgumentException ("pitch array must be of 128 elements.");
			if (LibFluidsynth.Synth.fluid_synth_create_octave_tuning (Handle, bank, prog, name, pitch) != 0)
				throw new FluidSynthInteropException ("key tuning create operation failed " + LastError);
		}

		public void ActivateOctaveTuning (int bank, int prog, string name, double [] pitch, bool apply)
		{
			if (pitch == null)
				throw new ArgumentNullException ("pitch");
			if (pitch.Length != 128)
				throw new ArgumentException ("pitch array must be of 128 elements.");
			if (LibFluidsynth.Synth.fluid_synth_activate_octave_tuning (Handle, bank, prog, name, pitch, apply) != 0)
				throw new FluidSynthInteropException ("key tuning create operation failed " + LastError);
		}

		public void TuneNotes (int bank, int prog, int[] keys, double[] pitch, bool apply)
		{
			if (keys == null)
				throw new ArgumentNullException ("keys");
			if (keys.Length != 128)
				throw new ArgumentException ("key array must be of 128 elements.");
			if (pitch == null)
				throw new ArgumentNullException ("pitch");
			if (pitch.Length != 128)
				throw new ArgumentException ("pitch array must be of 128 elements.");
			if (LibFluidsynth.Synth.fluid_synth_tune_notes (Handle, bank, prog, keys.Length, keys, pitch, apply) != 0)
				throw new FluidSynthInteropException ("key tuning create operation failed " + LastError);
		}

		public void SelectTuning (int channel, int bank, int prog)
		{
			if (LibFluidsynth.Synth.fluid_synth_select_tuning (Handle, channel, bank, prog) != 0)
				throw new FluidSynthInteropException ("tuning select operation failed " + LastError);
		}

		public void ActivateTuning (int channel, int bank, int prog, bool apply)
		{
			if (LibFluidsynth.Synth.fluid_synth_activate_tuning (Handle, channel, bank, prog, apply) != 0)
				throw new FluidSynthInteropException ("tuning activate operation failed " + LastError);
		}

		public void ResetTuning (int channel)
		{
			if (LibFluidsynth.Synth.fluid_synth_reset_tuning (Handle, channel) != 0)
				throw new FluidSynthInteropException ("tuning reset operation failed " + LastError);
		}

		public void DeactivateTuning (int channel, bool apply)
		{
			if (LibFluidsynth.Synth.fluid_synth_deactivate_tuning (Handle, channel, apply) != 0)
				throw new FluidSynthInteropException ("tuning deactivate operation failed " + LastError);
		}

		public void TuningIterationStart ()
		{
			LibFluidsynth.Synth.fluid_synth_tuning_iteration_start (Handle);
		}

		public bool TuningIterationNext (out int bank, out int prog)
		{
			return LibFluidsynth.Synth.fluid_synth_tuning_iteration_next (Handle, out bank, out prog);
		}

		public double [] TuningDump (int bank, int prog, out string name)
		{
			var ret = new double [128];
			byte [] nm = new byte [64];
			LibFluidsynth.Synth.fluid_synth_tuning_dump (Handle, bank, prog, nm, nm.Length, ret);
			name = Encoding.UTF8.GetString (nm, 0, nm.Length);
			return ret;
		}

		#endregion

		public double CpuLoad {
			get { return LibFluidsynth.Synth.fluid_synth_get_cpu_load (Handle); }
		}

		public string LastError {
			get {
				var ptr = LibFluidsynth.Synth.fluid_synth_error (Handle);
				return Marshal.PtrToStringAuto (ptr);
			}
		}

		public void WriteSample16 (int length, ushort [] leftOut, int leftOffset, int leftIncrement, ushort [] rightOut, int rightOffset, int rightIncrement)
		{
			if (LibFluidsynth.Synth.fluid_synth_write_s16 (Handle, length, leftOut, leftOffset, leftIncrement, rightOut, rightOffset, rightIncrement) != 0)
				throw new FluidSynthInteropException ("16bit sample write operation failed " + LastError);
		}

		public void WriteSampleFloat (int length, float [] leftOut, int leftOffset, int leftIncrement, float [] rightOut, int rightOffset, int rightIncrement)
		{
			if (LibFluidsynth.Synth.fluid_synth_write_float (Handle, length, leftOut, leftOffset, leftIncrement, rightOut, rightOffset, rightIncrement) != 0)
				throw new FluidSynthInteropException ("float sample write operation failed " + LastError);
		}

		public void WriteSampleFloat (int length, out float [] leftOut, out float [] rightOut)
		{
			float [] dummy, dummy2;
			if (LibFluidsynth.Synth.fluid_synth_nwrite_float (Handle, length, out leftOut, out rightOut, out dummy, out dummy2) != 0)
				throw new FluidSynthInteropException ("float sample write operation failed " + LastError);
		}

		public void Process (int length, int nIn, float [] inBuffer, int nOut, float [] outBuffer)
		{
			if (LibFluidsynth.Synth.fluid_synth_process (Handle, length, nIn, inBuffer, nOut, outBuffer) != 0)
				throw new FluidSynthInteropException ("float sample write operation failed " + LastError);
		}

		public void AddSoundFontLoader (IntPtr loader)
		{
			LibFluidsynth.Synth.fluid_synth_add_sfloader (Handle, loader);
		}

		public Voice AllocateVoice (IntPtr sample, int channel, int key, int vel)
		{
			var ret = LibFluidsynth.Synth.fluid_synth_alloc_voice (Handle, sample, channel, key, vel);
			if (ret == IntPtr.Zero)
				throw new FluidSynthInteropException ("voice allocate operation failed " + LastError);
			return new Voice (ret);
		}

		public void StartVoice (Voice voice)
		{
			LibFluidsynth.Synth.fluid_synth_start_voice (Handle, voice.Handle);
		}

		public bool GetVoiceList (Voice [] voices, int voiceId)
		{
			var arr = new IntPtr [voices.Length];
			LibFluidsynth.Synth.fluid_synth_get_voicelist (Handle, arr, arr.Length, voiceId);
			for (int i = 0; i < arr.Length; i++) {
				if (arr [i] == IntPtr.Zero)
					return false;
				voices [i] = new Voice (arr [i]);
			}
			return true;
		}

		public void HandleMidiEvent (IntPtr midiEvent)
		{
			if (LibFluidsynth.Synth.fluid_synth_handle_midi_event (Handle, midiEvent) != 0)
				throw new FluidSynthInteropException ("midi event handle operation failed " + LastError);
		}

		public void SetMidiRouter (IntPtr router)
		{
			LibFluidsynth.Synth.fluid_synth_set_midi_router (Handle, router);
		}
	}
}

