using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Shared.Tools
{
    // Not thread safe, since we don't care about super exact results
    public class CacheStat
    {
        private long Lookups { get; set; }
        private long Hits { get; set; }
        private int Size { get; set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            Reset(0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset(int size)
        {
            Lookups = 0;
            Hits = 0;
            Size = size;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void IncreaseSize(int size)
        {
            Size = Math.Max(Size, size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CountLookup(int size)
        {
            Size = size;
            Lookups++;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CountHit()
        {
            Hits++;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Add(CacheStat source)
        {
            Lookups += source.Lookups;
            Hits += source.Hits;
            Size += source.Size;

            source.Reset(source.Size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddRange(IEnumerable<CacheStat> stats)
        {
            foreach (var stat in stats)
                Add(stat);
        }

        public string Report
        {
            get
            {
                var lookups = Lookups;
                var hits = Hits;
                var size = Size;

                if (hits > lookups)
                    hits = lookups;

                Reset(size);

                var rate = lookups > 0 ? 100.0 * hits / lookups : 100.0;
                return $"HitRate = {rate:0.000}% = {hits}/{lookups}; ItemCount = {size}";
            }
        }
    }
}