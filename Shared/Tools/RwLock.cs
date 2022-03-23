using System.Runtime.CompilerServices;
using System.Threading;

namespace Shared.Tools
{
    public static class RwLock
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AcquireForReading(ref int counter)
        {
            var spinWait = new SpinWait();
            var previous = counter;
            while (previous < 0 || previous != Interlocked.CompareExchange(ref counter, previous + 1, previous))
            {
                spinWait.SpinOnce();
                previous = counter;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ReleaseAfterReading(ref int counter)
        {
            Interlocked.Decrement(ref counter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AcquireForWriting(ref int counter)
        {
            var spinWait = new SpinWait();
            while (Interlocked.CompareExchange(ref counter, -1, 0) != 0)
            {
                spinWait.SpinOnce();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ReleaseAfterWriting(ref int counter)
        {
            counter = 0;
        }
    }
}