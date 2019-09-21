using System;
using System.Runtime.InteropServices;
using fluid_settings_t_ptr = System.IntPtr;
using fluid_synth_t_ptr = System.IntPtr;
using fluid_preset_t_ptr = System.IntPtr;
using fluid_sfont_t_ptr = System.IntPtr;
using fluid_synth_channel_info_t_ptr = System.IntPtr;
using fluid_voice_t_ptr = System.IntPtr;
using fluid_midi_router_t_ptr = System.IntPtr;
using fluid_sfloader_t_ptr = System.IntPtr;
using fluid_sample_t_ptr = System.IntPtr;
using fluid_midi_event_t_ptr = System.IntPtr;

namespace NFluidsynth.Native
{
    internal static partial class LibFluidsynth
    {
        private delegate int fluid_audio_callback_t(fluid_synth_t_ptr synth, [MarshalAs(UnmanagedType.SysInt)] int len,
            IntPtr out1, [MarshalAs(UnmanagedType.SysInt)] int loff, [MarshalAs(UnmanagedType.SysInt)] int lincr,
            IntPtr out2, [MarshalAs(UnmanagedType.SysInt)] int roff, [MarshalAs(UnmanagedType.SysInt)] int rincr);


        [DllImport(LibraryName)]
        internal static extern fluid_synth_t_ptr new_fluid_synth(fluid_settings_t_ptr settings);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int delete_fluid_synth(fluid_synth_t_ptr synth);

        [DllImport(LibraryName)]
        internal static extern fluid_settings_t_ptr fluid_synth_get_settings(fluid_synth_t_ptr synth);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_noteon(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] int chan, [MarshalAs(UnmanagedType.SysInt)] int key,
            [MarshalAs(UnmanagedType.SysInt)] int vel);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_noteoff(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] int chan, [MarshalAs(UnmanagedType.SysInt)] int key);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_cc(fluid_synth_t_ptr synth, [MarshalAs(UnmanagedType.SysInt)] int chan,
            [MarshalAs(UnmanagedType.SysInt)] int ctrl, [MarshalAs(UnmanagedType.SysInt)] int val);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_get_cc(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] int chan, [MarshalAs(UnmanagedType.SysInt)] int ctrl,
            [MarshalAs(UnmanagedType.SysInt)] out int pval);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_sysex(fluid_synth_t_ptr synth, byte[] data,
            [MarshalAs(UnmanagedType.SysInt)] int len, byte[] response,
            [MarshalAs(UnmanagedType.SysInt)] ref int response_len, [MarshalAs(UnmanagedType.SysInt)] out bool handled,
            [MarshalAs(UnmanagedType.SysInt)] bool dryrun);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_pitch_bend(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] int chan, [MarshalAs(UnmanagedType.SysInt)] int val);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_get_pitch_bend(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] int chan, [MarshalAs(UnmanagedType.SysInt)] out int ppitch_bend);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_pitch_wheel_sens(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] int chan, [MarshalAs(UnmanagedType.SysInt)] int val);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_get_pitch_wheel_sens(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] int chan, [MarshalAs(UnmanagedType.SysInt)] out int pval);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_program_change(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] int chan, [MarshalAs(UnmanagedType.SysInt)] int program);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_channel_pressure(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] int chan, [MarshalAs(UnmanagedType.SysInt)] int val);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_key_pressure(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] int chan, [MarshalAs(UnmanagedType.SysInt)] int key,
            [MarshalAs(UnmanagedType.SysInt)] int val);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_bank_select(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] int chan, [MarshalAs(UnmanagedType.SysUInt)] uint bank);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_sfont_select(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] int chan, [MarshalAs(UnmanagedType.SysUInt)] uint sfont_id);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_program_select(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] int chan, [MarshalAs(UnmanagedType.SysUInt)] uint sfont_id,
            [MarshalAs(UnmanagedType.SysUInt)] uint bank_num, [MarshalAs(UnmanagedType.SysUInt)] uint preset_num);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_program_select_by_sfont_name(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] int chan, string sfont_name,
            [MarshalAs(UnmanagedType.SysUInt)] uint bank_num, [MarshalAs(UnmanagedType.SysUInt)] uint preset_num);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_get_program(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] int chan, [MarshalAs(UnmanagedType.SysUInt)] out int sfont_id,
            [MarshalAs(UnmanagedType.SysUInt)] out int bank_num, [MarshalAs(UnmanagedType.SysUInt)] out int preset_num);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_unset_program(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] int chan);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_get_channel_info(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] int chan, out fluid_synth_channel_info_t_ptr info);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_program_reset(fluid_synth_t_ptr synth);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_system_reset(fluid_synth_t_ptr synth);

        [DllImport(LibraryName)]
        internal static extern fluid_preset_t_ptr fluid_synth_get_channel_preset(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] int chan);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_start(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysUInt)] uint id, fluid_preset_t_ptr preset,
            [MarshalAs(UnmanagedType.SysInt)] int audio_chan, [MarshalAs(UnmanagedType.SysInt)] int midi_chan,
            [MarshalAs(UnmanagedType.SysInt)] int key, [MarshalAs(UnmanagedType.SysInt)] int vel);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int
            fluid_synth_stop(fluid_synth_t_ptr synth, [MarshalAs(UnmanagedType.SysUInt)] uint id);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_sfload(fluid_synth_t_ptr synth, string filename,
            [MarshalAs(UnmanagedType.SysInt)] bool reset_presets);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_sfreload(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysUInt)] uint id);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_sfunload(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysUInt)] uint id, [MarshalAs(UnmanagedType.SysInt)] bool reset_presets);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_add_sfont(fluid_synth_t_ptr synth, fluid_sfont_t_ptr sfont);

        [DllImport(LibraryName)]
        internal static extern void fluid_synth_remove_sfont(fluid_synth_t_ptr synth, fluid_sfont_t_ptr sfont);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_sfcount(fluid_synth_t_ptr synth);

        [DllImport(LibraryName)]
        internal static extern fluid_sfont_t_ptr fluid_synth_get_sfont(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysUInt)] uint num);

        [DllImport(LibraryName)]
        internal static extern fluid_sfont_t_ptr fluid_synth_get_sfont_by_id(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysUInt)] uint id);

        [DllImport(LibraryName)]
        internal static extern fluid_sfont_t_ptr fluid_synth_get_sfont_by_name(fluid_synth_t_ptr synth, string name);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_set_bank_offset(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] int sfont_id, [MarshalAs(UnmanagedType.SysInt)] int offset);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_get_bank_offset(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] int sfont_id);

        [DllImport(LibraryName)]
        internal static extern void fluid_synth_set_reverb(fluid_synth_t_ptr synth, double roomsize, double damping,
            double width, double level);

        [DllImport(LibraryName)]
        internal static extern void fluid_synth_set_reverb_on(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] bool on);

        [DllImport(LibraryName)]
        internal static extern double fluid_synth_get_reverb_roomsize(fluid_synth_t_ptr synth);

        [DllImport(LibraryName)]
        internal static extern double fluid_synth_get_reverb_damp(fluid_synth_t_ptr synth);

        [DllImport(LibraryName)]
        internal static extern double fluid_synth_get_reverb_level(fluid_synth_t_ptr synth);

        [DllImport(LibraryName)]
        internal static extern double fluid_synth_get_reverb_width(fluid_synth_t_ptr synth);

        [DllImport(LibraryName)]
        internal static extern void fluid_synth_set_chorus(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] int nr, double level, double speed, double depth_ms,
            [MarshalAs(UnmanagedType.SysInt)] FluidChorusMod type);

        [DllImport(LibraryName)]
        internal static extern void fluid_synth_set_chorus_on(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] bool on);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_get_chorus_nr(fluid_synth_t_ptr synth);

        [DllImport(LibraryName)]
        internal static extern double fluid_synth_get_chorus_level(fluid_synth_t_ptr synth);

        [DllImport(LibraryName)]
        internal static extern double fluid_synth_get_chorus_speed_Hz(fluid_synth_t_ptr synth);

        [DllImport(LibraryName)]
        internal static extern double fluid_synth_get_chorus_depth_ms(fluid_synth_t_ptr synth);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern FluidChorusMod fluid_synth_get_chorus_type(fluid_synth_t_ptr synth);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_count_midi_channels(fluid_synth_t_ptr synth);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_count_audio_channels(fluid_synth_t_ptr synth);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_count_audio_groups(fluid_synth_t_ptr synth);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_count_effects_channels(fluid_synth_t_ptr synth);

        [DllImport(LibraryName)]
        internal static extern void fluid_synth_set_sample_rate(fluid_synth_t_ptr synth, float sample_rate);

        [DllImport(LibraryName)]
        internal static extern void fluid_synth_set_gain(fluid_synth_t_ptr synth, float gain);

        [DllImport(LibraryName)]
        internal static extern float fluid_synth_get_gain(fluid_synth_t_ptr synth);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_set_polyphony(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] int polyphony);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_get_polyphony(fluid_synth_t_ptr synth);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_get_active_voice_count(fluid_synth_t_ptr synth);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_get_internal_bufsize(fluid_synth_t_ptr synth);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_set_interp_method(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] int chan,
            [MarshalAs(UnmanagedType.SysInt)] FluidInterpolation interp_method);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_set_gen(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] int chan, [MarshalAs(UnmanagedType.SysInt)] int param, float value);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_set_gen2(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] int chan, [MarshalAs(UnmanagedType.SysInt)] int param, float value,
            [MarshalAs(UnmanagedType.SysInt)] bool absolute, [MarshalAs(UnmanagedType.SysInt)] bool normalized);

        [DllImport(LibraryName)]
        internal static extern float fluid_synth_get_gen(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] int chan, [MarshalAs(UnmanagedType.SysInt)] int param);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_create_key_tuning(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] int bank, [MarshalAs(UnmanagedType.SysInt)] int prog, string name,
            double[] pitch);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_activate_key_tuning(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] int bank, [MarshalAs(UnmanagedType.SysInt)] int prog, string name,
            double[] pitch, [MarshalAs(UnmanagedType.SysInt)] bool apply);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_create_octave_tuning(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] int bank, [MarshalAs(UnmanagedType.SysInt)] int prog, string name,
            double[] pitch);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_activate_octave_tuning(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] int bank, [MarshalAs(UnmanagedType.SysInt)] int prog, string name,
            double[] pitch, [MarshalAs(UnmanagedType.SysInt)] bool apply);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_tune_notes(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] int bank, [MarshalAs(UnmanagedType.SysInt)] int prog,
            [MarshalAs(UnmanagedType.SysInt)] int len, int[] keys, double[] pitch,
            [MarshalAs(UnmanagedType.SysInt)] bool apply);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_select_tuning(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] int chan, [MarshalAs(UnmanagedType.SysInt)] int bank,
            [MarshalAs(UnmanagedType.SysInt)] int prog);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_activate_tuning(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] int chan, [MarshalAs(UnmanagedType.SysInt)] int bank,
            [MarshalAs(UnmanagedType.SysInt)] int prog, [MarshalAs(UnmanagedType.SysInt)] bool apply);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_reset_tuning(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] int chan);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_deactivate_tuning(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] int chan, [MarshalAs(UnmanagedType.SysInt)] bool apply);

        [DllImport(LibraryName)]
        internal static extern void fluid_synth_tuning_iteration_start(fluid_synth_t_ptr synth);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern bool fluid_synth_tuning_iteration_next(fluid_synth_t_ptr synth, out int bank,
            out int prog);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_tuning_dump(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] int bank, [MarshalAs(UnmanagedType.SysInt)] int prog, byte[] name,
            [MarshalAs(UnmanagedType.SysInt)] int len, double[] pitch);

        [DllImport(LibraryName)]
        internal static extern double fluid_synth_get_cpu_load(fluid_synth_t_ptr synth);

        [DllImport(LibraryName)]
        internal static extern IntPtr fluid_synth_error(fluid_synth_t_ptr synth);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_write_s16(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] int len, ushort[] lout, [MarshalAs(UnmanagedType.SysInt)] int loff,
            [MarshalAs(UnmanagedType.SysInt)] int lincr, ushort[] rout, [MarshalAs(UnmanagedType.SysInt)] int roff,
            [MarshalAs(UnmanagedType.SysInt)] int rincr);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_write_float(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] int len, float[] lout, [MarshalAs(UnmanagedType.SysInt)] int loff,
            [MarshalAs(UnmanagedType.SysInt)] int lincr, float[] rout, [MarshalAs(UnmanagedType.SysInt)] int roff,
            [MarshalAs(UnmanagedType.SysInt)] int rincr);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_nwrite_float(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] int len, out float[] left, out float[] right, out float[] fx_left,
            out float[] fx_right);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_process(fluid_synth_t_ptr synth,
            [MarshalAs(UnmanagedType.SysInt)] int len, [MarshalAs(UnmanagedType.SysInt)] int nin, float[] in_ignored,
            [MarshalAs(UnmanagedType.SysInt)] int nout, float[] outBuffer);

        [DllImport(LibraryName)]
        internal static extern void fluid_synth_add_sfloader(fluid_synth_t_ptr synth, fluid_sfloader_t_ptr loader);

        [DllImport(LibraryName)]
        internal static extern fluid_voice_t_ptr fluid_synth_alloc_voice(fluid_synth_t_ptr synth,
            fluid_sample_t_ptr sample, [MarshalAs(UnmanagedType.SysInt)] int channum,
            [MarshalAs(UnmanagedType.SysInt)] int key, [MarshalAs(UnmanagedType.SysInt)] int vel);

        [DllImport(LibraryName)]
        internal static extern void fluid_synth_start_voice(fluid_synth_t_ptr synth, fluid_voice_t_ptr voice);

        [DllImport(LibraryName)]
        internal static extern void fluid_synth_get_voicelist(fluid_synth_t_ptr synth, IntPtr[] buf,
            [MarshalAs(UnmanagedType.SysInt)] int bufsize, [MarshalAs(UnmanagedType.SysInt)] int ID);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_synth_handle_midi_event(IntPtr data, fluid_midi_event_t_ptr event_);

        [DllImport(LibraryName)]
        internal static extern void
            fluid_synth_set_midi_router(fluid_synth_t_ptr synth, fluid_midi_router_t_ptr router);
    }
}
