using System.Linq;
using System.Runtime.CompilerServices;
using Shared.Plugin;
using Shared.Tools;

namespace TorchPlugin.Shared.Tools
{
    public class UintCache<TK> where TK: struct
    {
        // Tick counter in the upper 32 bits
        private readonly ulong cleanupPeriod;
        private ulong tick;
        private ulong nextCleanup;

        // Supports deletion with no memory allocation
        private readonly uint maxDeleteCount;
        private readonly TK[] keysToDelete;

        private readonly RwLockDictionary<TK, ulong> cache = new RwLockDictionary<TK, ulong>();

        public UintCache(uint cleanupPeriod, uint maxDeleteCount)
        {
            this.cleanupPeriod = (ulong)cleanupPeriod << 32;
            this.maxDeleteCount = maxDeleteCount;

            keysToDelete = new TK[this.maxDeleteCount];
            nextCleanup = tick + cleanupPeriod;
        }

        // Must be called on every tick to to store the clock, but it does a cleanup only rarely
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clean()
        {
            if ((tick = (ulong)Common.Plugin.Tick << 32) != nextCleanup)
                return;

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
            cache.BeginReading();
            if (cache.TryGetValue(key, out var item) && item >= tick)
            {
                value = (uint)item;
                cache.FinishReading();
                return true;
            }

            cache.FinishReading();
            value = 0u;
            return false;
        }
    }
}