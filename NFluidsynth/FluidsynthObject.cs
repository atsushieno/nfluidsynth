using System;
using System.Runtime.CompilerServices;

namespace NFluidsynth
{
    public abstract class FluidsynthObject : IDisposable
    {
        private readonly bool _disposeRequired;

        protected FluidsynthObject(IntPtr handle, bool disposeRequired)
        {
            Handle = handle;
            _disposeRequired = disposeRequired;
        }

        public bool Disposed { get; private set; }
        internal IntPtr Handle { get; }

        protected abstract void OnDispose();

        public void Dispose()
        {
            if (_disposeRequired)
            {
                OnDispose();
            }

            Disposed = true;
        }

        public override int GetHashCode()
        {
            return (int) Handle;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as FluidsynthObject);
        }

        protected virtual bool Equals(FluidsynthObject obj)
        {
            return obj != null && Handle == obj.Handle;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void ThrowIfDisposed()
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(nameof(FluidsynthObject));
            }
        }
    }
}
