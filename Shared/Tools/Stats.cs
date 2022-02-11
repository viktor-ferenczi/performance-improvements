using System.Runtime.CompilerServices;
using System.Threading;

namespace Shared.Patches
{
    // ReSharper disable once MemberCanBePrivate.Global
    public struct Stats
    {
        public long Count;
        public long Max;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Increment()
        {
            Interlocked.Increment(ref Count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void UpdateMax(long count)
        {
            Interlocked.CompareExchange(ref Max, count, count - 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
        {
            Count = 0;
            Max = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string Format(float seconds)
        {
            return $"{Count / seconds:0.00}/s";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string Format(float seconds, long waits)
        {
            return $"{Count / seconds:0.00}/s, {Count / (float)waits:0.00}/w, {Max} max";
        }
    }
}