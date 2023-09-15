#if DEBUG

using System.Runtime.CompilerServices;

namespace Shared.Patches
{
    public class PullItemStats
    {
        public long PullItemCount;
        public long PullItemsCount;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string Report()
        {
            var pullItemCount = PullItemCount;
            var pullItemsCount = PullItemsCount;

            Reset();

            return $"PullItem {pullItemCount}; PullItems {pullItemsCount}";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Reset()
        {
            PullItemCount = 0;
            PullItemsCount = 0;
        }
    }
}

#endif