using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Shared.Plugin;
using Shared.Tools;

namespace TorchPlugin.Shared.Tools
{
    public class BoolCache
    {
        private readonly RwLockDictionary<long, long> cache = new RwLockDictionary<long, long>();

        private readonly long minimumExpiration; // ticks
        private readonly long cleanupPeriod; // ticks

        private readonly int maxDeleteCount;
        private readonly long[] keysToDelete;

        private long tick;

        public BoolCache(long minimumExpiration, long cleanupPeriod, int maxDeleteCount)
        {
            this.minimumExpiration = minimumExpiration;
            this.cleanupPeriod = cleanupPeriod;
            this.maxDeleteCount = maxDeleteCount;

            keysToDelete = new long[this.maxDeleteCount];
        }

        public void Clean()
        {
            tick = Common.Plugin.Tick;
            if (tick % cleanupPeriod != 0)
                return;

            var count = 0;
            cache.BeginReading();
            try
            {
                foreach (var (entityId, value) in cache)
                {
                    if (Math.Abs(value) > tick)
                        continue;

                    keysToDelete[count++] = entityId;
                    if (count == maxDeleteCount)
                        break;
                }
            }
            finally
            {
                cache.FinishReading();
            }

            if (count == 0)
                return;

            // No try-finally, because Remove cannot fail
            cache.BeginWriting();
            for (var i = 0; i < count; i++)
                cache.Remove(keysToDelete[i]);
            cache.FinishWriting();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Store(long key, bool result)
        {
            var expires = tick + minimumExpiration + (key & 7);
            cache.BeginWriting();
            cache[key] = result ? expires : -expires;
            cache.FinishWriting();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(long key, out bool result)
        {
            cache.BeginReading();
            if (cache.TryGetValue(key, out var value) && Math.Abs(value) <= tick)
            {
                result = value >= 0;
                cache.FinishReading();
                return true;
            }
            cache.FinishReading();
            result = false;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            cache.BeginWriting();
            cache.Clear();
            cache.FinishWriting();
        }
    }
}