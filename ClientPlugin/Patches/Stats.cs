using System.Runtime.CompilerServices;
using System.Threading;

namespace ClientPlugin.Patches
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
        public string Format(int seconds)
        {
            return $"{(Count + (seconds >> 1)) / seconds}/s";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string Format(int seconds, long waits)
        {
            return $"{(Count + (seconds >> 1)) / seconds}/s, {(Count + (waits >> 1)) / waits}/w, {Max} max";
        }
    }
}