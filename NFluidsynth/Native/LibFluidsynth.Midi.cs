using System;
using System.Runtime.InteropServices;
using fluid_midi_event_t_ptr = System.IntPtr;
using fluid_midi_router_t_ptr = System.IntPtr;
using fluid_player_t_ptr = System.IntPtr;
using fluid_settings_t_ptr = System.IntPtr;
using fluid_midi_router_rule_t_ptr = System.IntPtr;
using fluid_midi_driver_t_ptr = System.IntPtr;
using fluid_synth_t_ptr = System.IntPtr;

namespace NFluidsynth.Native
{
    internal static partial class LibFluidsynth
    {
        internal delegate int handle_midi_event_func_t(byte[] data, fluid_midi_event_t_ptr evt);

        [DllImport(LibraryName)]
        internal static extern fluid_midi_event_t_ptr new_fluid_midi_event();

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int delete_fluid_midi_event(fluid_midi_event_t_ptr evt);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_midi_event_set_type(fluid_midi_event_t_ptr evt,
            [MarshalAs(UnmanagedType.SysInt)] int type);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_midi_event_get_type(fluid_midi_event_t_ptr evt);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_midi_event_set_channel(fluid_midi_event_t_ptr evt,
            [MarshalAs(UnmanagedType.SysInt)] int chan);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_midi_event_get_channel(fluid_midi_event_t_ptr evt);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_midi_event_get_key(fluid_midi_event_t_ptr evt);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_midi_event_set_key(fluid_midi_event_t_ptr evt,
            [MarshalAs(UnmanagedType.SysInt)] int key);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_midi_event_get_velocity(fluid_midi_event_t_ptr evt);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_midi_event_set_velocity(fluid_midi_event_t_ptr evt,
            [MarshalAs(UnmanagedType.SysInt)] int vel);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_midi_event_get_control(fluid_midi_event_t_ptr evt);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_midi_event_set_control(fluid_midi_event_t_ptr evt,
            [MarshalAs(UnmanagedType.SysInt)] int ctrl);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_midi_event_get_value(fluid_midi_event_t_ptr evt);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_midi_event_set_value(fluid_midi_event_t_ptr evt,
            [MarshalAs(UnmanagedType.SysInt)] int val);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_midi_event_get_program(fluid_midi_event_t_ptr evt);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_midi_event_set_program(fluid_midi_event_t_ptr evt,
            [MarshalAs(UnmanagedType.SysInt)] int val);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_midi_event_get_pitch(fluid_midi_event_t_ptr evt);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_midi_event_set_pitch(fluid_midi_event_t_ptr evt,
            [MarshalAs(UnmanagedType.SysInt)] int val);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_midi_event_set_sysex(fluid_midi_event_t_ptr evt, byte[] data,
            [MarshalAs(UnmanagedType.SysInt)] int size, [MarshalAs(UnmanagedType.SysInt)] bool isDynamic);

        [DllImport(LibraryName)]
        internal static extern fluid_midi_router_t_ptr new_fluid_midi_router(fluid_settings_t_ptr settings,
            handle_midi_event_func_t handler, byte[] event_handler_data);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int delete_fluid_midi_router(fluid_midi_router_t_ptr handler);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_midi_router_set_default_rules(fluid_midi_router_t_ptr router);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_midi_router_clear_rules(fluid_midi_router_t_ptr router);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_midi_router_add_rule(fluid_midi_router_t_ptr router,
            fluid_midi_router_rule_t_ptr rule, [MarshalAs(UnmanagedType.SysInt)] FluidMidiRouterRuleType type);

        [DllImport(LibraryName)]
        internal static extern fluid_midi_router_rule_t_ptr new_fluid_midi_router_rule();

        [DllImport(LibraryName)]
        internal static extern void delete_fluid_midi_router_rule(fluid_midi_router_rule_t_ptr rule);

        [DllImport(LibraryName)]
        internal static extern void fluid_midi_router_rule_set_chan(fluid_midi_router_rule_t_ptr rule,
            [MarshalAs(UnmanagedType.SysInt)] int min, [MarshalAs(UnmanagedType.SysInt)] int max, float mul,
            [MarshalAs(UnmanagedType.SysInt)] int add);

        [DllImport(LibraryName)]
        internal static extern void fluid_midi_router_rule_set_param1(fluid_midi_router_rule_t_ptr rule,
            [MarshalAs(UnmanagedType.SysInt)] int min, [MarshalAs(UnmanagedType.SysInt)] int max, float mul,
            [MarshalAs(UnmanagedType.SysInt)] int add);

        [DllImport(LibraryName)]
        internal static extern void fluid_midi_router_rule_set_param2(fluid_midi_router_rule_t_ptr rule,
            [MarshalAs(UnmanagedType.SysInt)] int min, [MarshalAs(UnmanagedType.SysInt)] int max, float mul,
            [MarshalAs(UnmanagedType.SysInt)] int add);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_midi_router_handle_midi_event(IntPtr data, fluid_midi_event_t_ptr evt);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_midi_dump_prerouter(IntPtr data, fluid_midi_event_t_ptr evt);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_midi_dump_postrouter(IntPtr data, fluid_midi_event_t_ptr evt);

        [DllImport(LibraryName)]
        internal static extern fluid_midi_driver_t_ptr new_fluid_midi_driver(fluid_settings_t_ptr settings,
            handle_midi_event_func_t handler, byte[] event_handler_data);

        [DllImport(LibraryName)]
        internal static extern void delete_fluid_midi_driver(fluid_midi_driver_t_ptr driver);

        [DllImport(LibraryName)]
        internal static extern fluid_player_t_ptr new_fluid_player(fluid_synth_t_ptr synth);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int delete_fluid_player(fluid_player_t_ptr player);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_player_add(fluid_player_t_ptr player, string midifile);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_player_play(fluid_player_t_ptr player);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_player_stop(fluid_player_t_ptr player);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_player_join(fluid_player_t_ptr player);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_player_set_loop(fluid_player_t_ptr player,
            [MarshalAs(UnmanagedType.SysInt)] int loop);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_player_set_midi_tempo(fluid_player_t_ptr player,
            [MarshalAs(UnmanagedType.SysInt)] int tempo);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern int fluid_player_set_bpm(fluid_player_t_ptr player,
            [MarshalAs(UnmanagedType.SysInt)] int bpm);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern FluidPlayerStatus fluid_player_get_status(fluid_player_t_ptr player);
    }
}
