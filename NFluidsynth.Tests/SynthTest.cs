using System;
using NUnit.Framework;
using System.IO;
using NFluidsynth;

namespace NFluidsynth.Tests
{
    // Right now the tests are written totally under my Ubuntu 14.04 environment. Not really working outside of it.
    [TestFixture]
    public class SynthTest
    {
        Settings NewAlsaSettings()
        {
            var s = new Settings();
            s[ConfigurationKeys.AudioDriver].StringValue = "alsa";
            return s;
        }

        [Test]
        public void Properties()
        {
            using (var syn = new Synth(NewAlsaSettings()))
            {
                Assert.AreEqual(0, syn.FontCount, "FontCount");
                Assert.AreEqual(0, syn.ActiveVoiceCount, "ActiveVoiceCount");
                Assert.AreEqual(1, syn.AudioChannelCount, "AudioChannelCount");
                Assert.AreEqual(1, syn.AudioGroupCount, "AudioGroupCount");
                Assert.AreEqual(64, syn.InternalBufferSize, "InternalBufferSize");
                Assert.AreEqual(16, syn.MidiChannelCount, "MidiChannelCount");
                Assert.AreEqual(0, syn.NumberOfChorusVoices, "NumberOfChorusVoices");
                Assert.AreEqual(256, syn.Polyphony, "Polyphony");
            }
        }

        [Test]
        public void SystemReset()
        {
            using (var syn = new Synth(NewAlsaSettings()))
                syn.SystemReset();
        }

        [Test]
        public void CreateAudioDriver()
        {
            using (var syn = new Synth(NewAlsaSettings()))
            using (var audio = new AudioDriver(syn.Settings, syn))
                TextWriter.Null.WriteLine();
        }

        [Test]
        public void LoadUnloadSoundFont()
        {
            using (var syn = new Synth(NewAlsaSettings()))
            using (var audio = new AudioDriver(syn.Settings, syn))
            {
                syn.LoadSoundFont("/usr/share/sounds/sf2/FluidR3_GS.sf2", false);
                Assert.AreEqual(1, syn.FontCount, "FontCount");
                for (int i = 0; i < 16; i++)
                    syn.SoundFontSelect(i, 1);
                syn.UnloadSoundFont(1, true);
                Assert.AreEqual(0, syn.FontCount, "FontCount");
            }
        }
    }
}
