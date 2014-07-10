using NUnit.Framework;
using System;
using NFluidsynth;

namespace NFluidsynth.Tests
{
	[TestFixture]
	public class SettingsTest
	{
		[Test]
		public void Properties ()
		{
			var c = new Settings ();
			Assert.AreEqual (16, c [ConfigurationKeys.SynthMidiChannels].IntValue, ConfigurationKeys.SynthMidiChannels);
			Assert.AreEqual (1, c [ConfigurationKeys.SynthAudioChannels].IntValue, ConfigurationKeys.SynthAudioChannels);
			Assert.AreEqual ("alsa_seq", c [ConfigurationKeys.MidiDriver].StringValue, ConfigurationKeys.MidiDriver);
			Assert.AreEqual ("jack", c [ConfigurationKeys.AudioDriver].StringValue, ConfigurationKeys.AudioDriver);
			Assert.AreEqual (0, c [ConfigurationKeys.SynthDeviceId].IntValue, ConfigurationKeys.SynthDeviceId);
			c.Dispose ();
		}
	}
}
