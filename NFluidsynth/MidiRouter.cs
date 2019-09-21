using System;
using NFluidsynth.Native;

namespace NFluidsynth
{
    public delegate int MidiEventHandler(byte[] data, MidiEvent evt);

    public class MidiRouter : FluidsynthObject
    {
        public MidiRouter(Settings settings, MidiEventHandler handler)
            : base(LibFluidsynth.new_fluid_midi_router(settings.Handle, (d, e) => handler(d, new MidiEvent(e)), null),
                true)
        {
        }

        protected override void OnDispose()
        {
            LibFluidsynth.delete_fluid_midi_router(Handle);
        }

        public void SetDefaultRules()
        {
            LibFluidsynth.fluid_midi_router_set_default_rules(Handle);
        }

        public void ClearRules()
        {
            LibFluidsynth.fluid_midi_router_clear_rules(Handle);
        }

        public void AddRule(MidiRouterRule rule, FluidMidiRouterRuleType type)
        {
            LibFluidsynth.fluid_midi_router_add_rule(Handle, rule.Handle, type);
        }
    }
}
