using System.Runtime.CompilerServices;
using System.Threading;

namespace Shared.Patches
{
    // ReSharper disable once MemberCanBePrivate.Global
    public struct Stats
    {
        public long Count;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Increment()
        {
            Interlocked.Increment(ref Count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
        {
            Interlocked.Exchange(ref Count, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string Format(float seconds)
        {
            return $"{Count / seconds:0.00}/s";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string Format(float seconds, long waits)
        {
            return $"{Count / seconds:0.00}/s, {Count / (float)waits:0.00}/w";
        }
    }
}