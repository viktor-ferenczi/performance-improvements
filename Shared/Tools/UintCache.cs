using System.Linq;
using System.Runtime.CompilerServices;
using Shared.Plugin;

namespace Shared.Tools
{
    public class UintCache<TK>
    {
        private readonly int cleanupAbove;

        // The tick counter is in the upper 32 bits of these values
        private readonly ulong cleanupPeriod;
        private ulong tick;
        private ulong nextCleanup;

        // Supports deletion with no memory allocation
        private readonly uint maxDeleteCount;
        private readonly TK[] keysToDelete;

        private readonly RwLockDictionary<TK, ulong> cache = new RwLockDictionary<TK, ulong>();

#if DEBUG
        public readonly CacheStat Stat = new CacheStat();
        public bool HasItems => cache.Count != 0;
        public string Report => Stat.Report;
#endif

        public UintCache(uint cleanupPeriod, uint maxDeleteCount = 64)
        {
            this.cleanupPeriod = (ulong)cleanupPeriod << 32;
            this.maxDeleteCount = maxDeleteCount;

            // FIXME: Reuse arrays of the same size (get them from a thread local pool on demand)
            keysToDelete = new TK[this.maxDeleteCount];
            nextCleanup = tick + cleanupPeriod;
        }

        // Must be called on every tick to to store the clock, but it does a cleanup only rarely
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Cleanup()
        {
            // We expect reading the number of items to be atomic.
            // Getting a wrong value sometimes does not break anything anyway.
            if (cache.Count <= cleanupAbove)
                return false;

            if ((tick = (ulong)Common.Plugin.Tick << 32) < nextCleanup)
                return false;

            nextCleanup = tick + cleanupPeriod;

            var count = 0u;
            cache.BeginReading();
            foreach (var (key, item) in cache)
            {
                if (item >= tick)
                    continue;

                keysToDelete[count++] = key;
                if (count == maxDeleteCount)
                    break;
            }

            cache.FinishReading();
            if (count == 0)
                return false;

            cache.BeginWriting();
            for (var i = 0; i < count; i++)
                cache.Remove(keysToDelete[i]);
            cache.FinishWriting();

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            cache.BeginWriting();
            cache.Clear();
            cache.FinishWriting();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Store(TK key, uint value, uint lifetime)
        {
            var expires = tick + ((ulong)lifetime << 32);
            cache.BeginWriting();
            cache[key] = expires | value;
            cache.FinishWriting();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Extend(TK key, uint lifetime)
        {
            cache.BeginWriting();
            if (cache.TryGetValue(key, out var item))
                cache[key] = (tick + ((ulong)lifetime << 32)) | item & 0xfffffffful;
            cache.FinishWriting();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Forget(TK key)
        {
            cache.BeginWriting();
            cache.Remove(key);
            cache.FinishWriting();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(TK key, out uint value)
        {
#if DEBUG
            Stat.CountLookup(cache.Count);
#endif
            cache.BeginReading();
            if (cache.TryGetValue(key, out var item))
            {
                if (item >= tick)
                {
                    value = (uint)item;
                    cache.FinishReading();
#if DEBUG
                    Stat.CountHit();
#endif
                    return true;
                }
            }

            cache.FinishReading();
            value = 0u;
            return false;
        }
    }
}