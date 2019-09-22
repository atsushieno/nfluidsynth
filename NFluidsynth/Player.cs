using NFluidsynth.Native;

namespace NFluidsynth
{
    public class Player : FluidsynthObject
    {
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

        public FluidPlayerStatus Status
        {
            get { return LibFluidsynth.fluid_player_get_status(Handle); }
        }
    }
}