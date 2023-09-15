using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Shared.Plugin;

namespace Shared.Tools
{
    public class Cache<TK, TV>
    {
        private long tick;
        private long nextCleanup;
        private readonly int cleanupPeriod;

        // Supports deletion with no memory allocation
        private readonly int maxDeleteCount;
        private readonly TK[] keysToDelete;

#if DEBUG
        public readonly CacheStat Stat = new CacheStat();
        public string Report => Stat.Report;
        public IEnumerable<TR> Map<TR>(Func<TV, TR> fn)
        {
            cache.BeginReading();
            foreach (var value in cache.Values)
                yield return fn(value.Value);
            cache.FinishReading();
        }

        public CacheStat Aggregate(IEnumerable<CacheStat> stats)
        {
            var stat = new CacheStat();
            cache.BeginReading();
            stat.AddRange(stats);
            cache.FinishReading();
            return stat;
        }
#endif

        private class Item
        {
            public TV Value;
            public long Expires;
        }

        private readonly RwLockDictionary<TK, Item> cache = new RwLockDictionary<TK, Item>();

        public Cache(int cleanupPeriod, int maxDeleteCount = 64)
        {
            this.cleanupPeriod = cleanupPeriod;
            this.maxDeleteCount = maxDeleteCount;

            // FIXME: Reuse arrays of the same size (get them from a thread local pool on demand)
            keysToDelete = new TK[this.maxDeleteCount];
            nextCleanup = tick + cleanupPeriod;
        }

        // Must be called on every tick to to store the clock, but it does a cleanup only rarely
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Cleanup()
        {
            if ((tick = Common.Plugin.Tick) < nextCleanup)
                return;

            nextCleanup = tick + cleanupPeriod;

            var count = 0u;
            cache.BeginReading();
            foreach (var (key, item) in cache)
            {
                if (item.Expires >= tick)
                    continue;

                keysToDelete[count++] = key;
                if (count == maxDeleteCount)
                    break;
            }

            cache.FinishReading();
            if (count == 0)
                return;

            cache.BeginWriting();
            for (var i = 0; i < count; i++)
                cache.Remove(keysToDelete[i]);
            cache.FinishWriting();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            cache.BeginWriting();
            cache.Clear();
            cache.FinishWriting();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Store(TK key, TV value, int lifetime)
        {
            cache.BeginWriting();
            cache[key] = new Item { Value = value, Expires = tick + lifetime };
            cache.FinishWriting();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Extend(TK key, int lifetime)
        {
            cache.BeginWriting();
            if (cache.TryGetValue(key, out var item))
                item.Expires = tick + lifetime;
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
        public bool TryGetValue(TK key, out TV value)
        {
#if DEBUG
            Stat.CountLookup(cache.Count);
#endif
            cache.BeginReading();
            if (cache.TryGetValue(key, out var item))
            {
                if (item.Expires >= tick)
                {
                    value = item.Value;
                    cache.FinishReading();
#if DEBUG
                    Stat.CountHit();
#endif
                    return true;
                }
            }

            cache.FinishReading();
            value = default;
            return false;
        }
    }
}