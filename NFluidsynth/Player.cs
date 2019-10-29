using System;
using NFluidsynth.Native;

namespace NFluidsynth
{
    public class Player : FluidsynthObject
    {
        // Keep this here so the GC doesn't erase it from existence
        private LibFluidsynth.handle_midi_event_func_t _handler;

        public Player(Synth synth)
            : base(LibFluidsynth.new_fluid_player(synth.Handle))
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                LibFluidsynth.delete_fluid_player(Handle);
            }

            base.Dispose(disposing);
        }

        public void Add(string midifile)
        {
            LibFluidsynth.fluid_player_add(Handle, midifile);
        }

        #if NET472 || NETCOREAPP
        public unsafe void AddMem(ReadOnlySpan<byte> buffer)
        {
            fixed (byte* ptr = buffer)
                AddMem ((IntPtr) ptr, buffer.Length);
        }
        #endif
        
        public unsafe void AddMem(byte [] buffer, int offset, int length)
        {
            fixed (byte* ptr = buffer)
                AddMem ((IntPtr) (ptr + offset), buffer.Length);
        }

        public void AddMem(IntPtr buffer, int length)
        {
            LibFluidsynth.fluid_player_add_mem(Handle, buffer, length);
        }

        public void Play()
        {
            LibFluidsynth.fluid_player_play(Handle);
        }

        public void Stop()
        {
            LibFluidsynth.fluid_player_stop(Handle);
        }

        public void Join()
        {
            LibFluidsynth.fluid_player_join(Handle);
        }

        public void SetLoop(int loop)
        {
            LibFluidsynth.fluid_player_set_loop(Handle, loop);
        }

        public void SetTempo(int tempo)
        {
            LibFluidsynth.fluid_player_set_midi_tempo(Handle, tempo);
        }

        public void SetBpm(int bpm)
        {
            LibFluidsynth.fluid_player_set_bpm(Handle, bpm);
        }

        public unsafe void SetPlaybackCallback(MidiEventHandler handler)
        {
            LibFluidsynth.fluid_player_set_playback_callback(Handle,
                Utility.PassDelegatePointer<LibFluidsynth.handle_midi_event_func_t>(
                    (d, e) =>
                    {
                        using (var ev = new MidiEvent(e))
                        {
                            return handler(ev);
                        }
                    }, out var b), null);
            _handler = b;
        }

        public FluidPlayerStatus Status => LibFluidsynth.fluid_player_get_status(Handle);
    }
}
