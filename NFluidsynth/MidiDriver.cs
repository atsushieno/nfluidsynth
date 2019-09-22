using static NFluidsynth.Native.LibFluidsynth;

namespace NFluidsynth
{
    public class MidiDriver : FluidsynthObject
    {
        private readonly handle_midi_event_func_t _handler;

        public unsafe MidiDriver(Settings settings, MidiEventHandler handler)
            : base(new_fluid_midi_driver(
                    settings.Handle,
                    Pass((d, e) => handler(new MidiEvent(e)), out var b), null),
                true)
        {
            _handler = b;
        }

        protected override void OnDispose()
        {
            delete_fluid_midi_driver(Handle);
        }

        private static handle_midi_event_func_t Pass(handle_midi_event_func_t a, out handle_midi_event_func_t b)
        {
            b = a;
            return b;
        }
    }
}