using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commons.Music.Midi;

namespace NFluidsynth.MidiManager
{
	public class FluidsynthMidiAccess : IMidiAccess
	{
		public FluidsynthMidiAccess ()
		{
			Soundfonts = new List<string> ();
			output = new FluidsynthMidiOutput (this);
		}

		public IEnumerable<IMidiInput> Inputs {
			get { return Enumerable.Empty<IMidiInput> (); }
		}

		public IEnumerable<IMidiOutput> Outputs {
			get { return Enumerable.Repeat (output, 1); }
		}

		public IList<string> Soundfonts { get; private set; }

		public event EventHandler<MidiConnectionEventArgs> StateChanged; // won't detect...

		FluidsynthMidiOutput output;
	}

	class FluidsynthMidiOutputDetails : IMidiPortDetails
	{
		public string Id {
			get { return "id0"; }
		}

		public string Manufacturer {
			get { return "fluidsynth project"; }
		}

		public string Name {
			get { return "Fluidsynth output port"; }
		}

		public string Version {
			get { return "v0.000"; }
		}
	}

	class FluidsynthMidiOutput : IMidiOutput
	{
		static Task completed_task = Task.FromResult (false);

		public FluidsynthMidiOutput (FluidsynthMidiAccess midiAccess)
		{
			if (midiAccess == null)
				throw new ArgumentNullException ("midiAccess");
			midi_access = midiAccess;
			Details = new FluidsynthMidiOutputDetails ();
		}

		public MidiPortConnectionState Connection { get; private set; }

		public IMidiPortDetails Details { get; private set; }

		public MidiPortDeviceState State { get; private set; }

		// it won't change...
		public event EventHandler StateChanged;

		FluidsynthMidiAccess midi_access;

		public IList<string> Soundfonts {
			get { return midi_access.Soundfonts; }
		}

		public Task CloseAsync ()
		{
			if (synth != null)
				synth.Dispose ();
			synth = null;
			if (settings != null)
				settings.Dispose ();
			settings = null;
			return Task.FromResult (true);
		}

		public void Dispose ()
		{
			CloseAsync ().Wait ();
		}

		Settings settings;
		Synth synth;

		public Task OpenAsync ()
		{
			if (settings != null)
				throw new InvalidOperationException ("The MIDI output is already open.");
			settings = new Settings ();
			synth = new Synth (settings);

			return Task.FromResult (true);
		}

		public Task SendAsync (byte [] msg, int length, long timestamp)
		{
			int ch = msg [0] & 0x0F;
			switch (msg [0] & 0xF0) {
			case 0x80:
				synth.NoteOff (ch, msg [1]);
				break;
			case 0x90:
				if (msg [2] == 0)
					synth.NoteOff (ch, msg [1]);
				else
					synth.NoteOn (ch, msg [1], msg [2]);
				break;
			case 0xA0:
				// No PAf in fluidsynth?
				break;
			case 0xB0:
				synth.CC (ch, msg [1], msg [2]);
				break;
			case 0xC0:
				synth.ProgramChange (ch, msg [1]);
				break;
			case 0xD0:
				synth.ChannelPressure (ch, msg [1]);
				break;
			case 0xE0:
				synth.PitchBend (ch, (msg [1] << 14) + msg [2]);
				break;
			case 0xF0:
				synth.Sysex (new ArraySegment<byte> (msg, 0, length).ToArray (), null);
				break;
			}
			return completed_task;
		}
	}
}

