using System;
using NFluidsynth.Native;
using System.Runtime.Serialization;

namespace NFluidsynth
{
    public class FluidSynthInteropException : Exception
    {
        public FluidSynthInteropException() : this("Fluidsynth native error")
        {
        }

        public FluidSynthInteropException(string message) : base(message)
        {
        }


#if !PORTABLE
        public FluidSynthInteropException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
#endif

        public FluidSynthInteropException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}