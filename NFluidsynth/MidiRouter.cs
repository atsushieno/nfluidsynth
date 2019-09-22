using System;
using NFluidsynth.Native;

namespace NFluidsynth
{
    public delegate int MidiEventHandler(MidiEvent evt);

    public class MidiRouter : FluidsynthObject
    {
        public unsafe MidiRouter(Settings settings, MidiEventHandler handler)
            : base(LibFluidsynth.new_fluid_midi_router(settings.Handle, (d, e) => handler(new MidiEvent(e)), null))
        {
        }

        protected override void Dispose(bool disposed)
        {
            if (!Disposed)
            {
                LibFluidsynth.delete_fluid_midi_router(Handle);
            }

            base.Dispose(disposed);
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