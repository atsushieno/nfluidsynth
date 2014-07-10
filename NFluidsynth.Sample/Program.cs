using System;
using NFluidsynth;
using System.Threading;
using System.Linq;

namespace NFluidsynth.Sample
{
	public class Program
	{
		public static void Main (string[] args)
		{
			using (var settings = new Settings ()) {
				if (Environment.OSVersion.Platform == PlatformID.Unix)
					settings [ConfigurationKeys.AudioDriver].StringValue = "alsa";
				using (var syn = new Synth (settings)) {
					foreach (var arg in args)
						if (Synth.IsSoundFont (arg))
							syn.LoadSoundFont (arg, true);
					if (syn.FontCount == 0)
						syn.LoadSoundFont ("/usr/share/sounds/sf2/FluidR3_GM.sf2", true);
					var files = args.Where (a => Synth.IsMidiFile (a));
					if (files.Any ()) {
						foreach (var arg in files) {
							using (var player = new Player (syn)) {
								using (var adriver = new AudioDriver (syn.Settings, syn)) {
									player.Add (arg);
									player.Play ();
									player.Join ();
								}
							}
						}
					} else {
						using (var adriver = new AudioDriver (syn.Settings, syn)) {
							syn.NoteOn (0, 60, 100);
							Thread.Sleep (5000);
							syn.NoteOff (0, 60);
						}
					}
				}
			}
		}
	}
}

