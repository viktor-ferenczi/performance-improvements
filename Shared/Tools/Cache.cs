using System.Linq;
using System.Runtime.CompilerServices;
using Shared.Plugin;
using Shared.Tools;

namespace TorchPlugin.Shared.Tools
{
    public class Cache<T>
    {
        private long tick;
        private long nextCleanup;
        private readonly int cleanupPeriod;

        // Supports deletion with no memory allocation
        private readonly int maxDeleteCount;
        private readonly long[] keysToDelete;

        private class Item
        {
            public T Value;
            public long Expires;
        }

        private readonly RwLockDictionary<long, Item> cache = new RwLockDictionary<long, Item>();

        public Cache(int cleanupPeriod, int maxDeleteCount)
        {
            this.cleanupPeriod = cleanupPeriod;
            this.maxDeleteCount = maxDeleteCount;

            keysToDelete = new long[this.maxDeleteCount];
            nextCleanup = tick + cleanupPeriod;
        }

        // Must be called on every tick to to store the clock, but it does a cleanup only rarely
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clean()
        {
            if ((tick = Common.Plugin.Tick) != nextCleanup)
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
        public void Store(long key, T value, int lifetime)
        {
            cache.BeginWriting();
            cache[key] = new Item { Value = value, Expires = tick + lifetime };
            cache.FinishWriting();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Extend(long key, int lifetime)
        {
            cache.BeginWriting();
            if (cache.TryGetValue(key, out var item))
                item.Expires = tick + lifetime;
            cache.FinishWriting();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Forget(long key)
        {
            cache.BeginWriting();
            cache.Remove(key);
            cache.FinishWriting();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(long key, out T value)
        {
            cache.BeginReading();
            if (cache.TryGetValue(key, out var item) && item.Expires >= tick)
            {
                value = item.Value;
                cache.FinishReading();
                return true;
            }

            cache.FinishReading();
            value = default;
            return false;
        }
    }
}