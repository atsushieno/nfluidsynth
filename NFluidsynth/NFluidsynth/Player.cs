using System;
using NFluidsynth.Native;

namespace NFluidsynth
{
	public class Player : FluidsynthObject
	{
		public Player (Synth synth)
			: base (LibFluidsynth.Midi.new_fluid_player (synth.Handle), true)
		{
		}

		protected internal Player (IntPtr handle)
			: base (handle, false)
		{
		}

		protected override void OnDispose ()
		{
			LibFluidsynth.Midi.delete_fluid_player (Handle);
		}

		public void Add (string midifile)
		{
			LibFluidsynth.Midi.fluid_player_add (Handle, midifile);
		}

		public void Play ()
		{
			LibFluidsynth.Midi.fluid_player_play (Handle);
		}

		public void Stop ()
		{
			LibFluidsynth.Midi.fluid_player_stop (Handle);
		}

		public void Join ()
		{
			LibFluidsynth.Midi.fluid_player_join (Handle);
		}

		public void SetLoop (int loop)
		{
			LibFluidsynth.Midi.fluid_player_set_loop (Handle, loop);
		}

		public void SetTempo (int tempo)
		{
			LibFluidsynth.Midi.fluid_player_set_midi_tempo (Handle, tempo);
		}

		public void SetBpm (int bpm)
		{
			LibFluidsynth.Midi.fluid_player_set_bpm (Handle, bpm);
		}

		public FluidPlayerStatus Status {
			get { return LibFluidsynth.Midi.fluid_player_get_status (Handle); }
		}
	}
}

