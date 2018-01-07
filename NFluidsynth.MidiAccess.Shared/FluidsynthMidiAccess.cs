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
			: this (4)
		{
		}
		public FluidsynthMidiAccess (int ports)
		{
			SoundFonts = new List<string> ();
			SoundFontLoaderFactories = new List<Func<Synth,SoundFontLoader>> ();
			foreach (var m in Enumerable.Range (1, ports + 1).Select (i => new FluidsynthMidiOutput (this, "id" + i)))
				outputs.Add (m.Details.Id, m);
		}
		
		Dictionary<string,FluidsynthMidiOutput> outputs = new Dictionary<string, FluidsynthMidiOutput> ();
		
		public Synth.ErrorHandler HandleNativeError { get; set; }
		
		public IEnumerable<IMidiPortDetails> Inputs {
			get { return Enumerable.Empty<IMidiPortDetails> (); }
		}

		public IEnumerable<IMidiPortDetails> Outputs {
			get { return outputs.Values.Select (m => m.Details); }
		}
		
		public Task<IMidiInput> OpenInputAsync (string portId)
		{
			throw new NotSupportedException ();
		}
		
		public Task<IMidiOutput> OpenOutputAsync (string portId)
		{
			FluidsynthMidiOutput output;
			if (!outputs.TryGetValue (portId, out output))
				throw new ArgumentException (string.Format ("Port {0} does not exist.", portId));
			return output.OpenAsync ().ContinueWith (t => (IMidiOutput) output);
		}
		
		public IList<string> SoundFonts { get; private set; }
		public IList<Func<Synth,SoundFontLoader>> SoundFontLoaderFactories { get; private set; }

		public Action<Settings> ConfigureSettings;

		public event EventHandler<MidiConnectionEventArgs> StateChanged; // won't detect...
	}

	class FluidsynthMidiOutputDetails : IMidiPortDetails
	{
		public FluidsynthMidiOutputDetails (string portId)
		{
			Id = portId;
		}

		public string Id { get; private set; }

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

		public FluidsynthMidiOutput (FluidsynthMidiAccess midiAccess, string portId)
		{
			if (midiAccess == null)
				throw new ArgumentNullException ("midiAccess");
			midi_access = midiAccess;
			Details = new FluidsynthMidiOutputDetails (portId);
		}

		public MidiPortConnectionState Connection { get; private set; }

		public IMidiPortDetails Details { get; private set; }

		FluidsynthMidiAccess midi_access;

		public Task CloseAsync ()
		{
			if (adriver != null)
				adriver.Dispose ();
			adriver = null;
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

		Synth synth;
		Settings settings;
		AudioDriver adriver;

		public Task OpenAsync ()
		{
			if (synth != null)
				throw new InvalidOperationException ("The MIDI output is already open.");
			settings = new Settings ();
			if (midi_access.ConfigureSettings != null)
				midi_access.ConfigureSettings (settings);
			synth = new Synth (settings);
			synth.HandleError = midi_access.HandleNativeError;
			foreach (var factory in midi_access.SoundFontLoaderFactories)
				synth.AddSoundFontLoader (factory (synth));
			foreach (var sf in midi_access.SoundFonts)
				synth.LoadSoundFont (sf, false);

			adriver = new AudioDriver (synth.Settings, synth);

			return Task.FromResult (true);
		}

		public void Send (byte [] msg, int offset, int length, long timestamp)
		{
			if (synth == null)
				throw new InvalidOperationException ("The MIDI output is not open.");
			
			int ch = msg [offset] & 0x0F;
			switch (msg [offset] & 0xF0) {
			case 0x80:
				synth.NoteOff (ch, msg [offset + 1]);
				break;
			case 0x90:
				if (msg [offset + 2] == 0)
					synth.NoteOff (ch, msg [offset + 1]);
				else
					synth.NoteOn (ch, msg [offset + 1], msg [offset + 2]);
				break;
			case 0xA0:
				// No PAf in fluidsynth?
				break;
			case 0xB0:
				synth.CC (ch, msg [offset + 1], msg [offset + 2]);
				break;
			case 0xC0:
				synth.ProgramChange (ch, msg [offset + 1]);
				break;
			case 0xD0:
				synth.ChannelPressure (ch, msg [offset + 1]);
				break;
			case 0xE0:
				synth.PitchBend (ch, msg [offset + 1] + msg [offset + 2] * 0x80);
				break;
			case 0xF0:
				synth.Sysex (new ArraySegment<byte> (msg, offset, length).ToArray (), null);
				break;
			}
		}
	}
}

