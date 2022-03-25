using System.Runtime.CompilerServices;
using Shared.Tools;

namespace TorchPlugin.Shared.Tools
{
    public class CacheForever<TK, TV>
    {
        private readonly RwLockDictionary<TK, TV> cache = new RwLockDictionary<TK, TV>();

#if DEBUG
        public readonly CacheStat Stat = new CacheStat();
        public string Report => Stat.Report;
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            cache.BeginWriting();
            cache.Clear();
            cache.FinishWriting();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Store(TK key, TV value)
        {
            cache.BeginWriting();
            cache[key] = value;
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
            if (cache.TryGetValue(key, out value))
            {
                cache.FinishReading();
#if DEBUG
                Stat.CountHit();
#endif
                return true;
            }

            cache.FinishReading();
            value = default;
            return false;
        }
    }
}