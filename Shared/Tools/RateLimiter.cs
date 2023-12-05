namespace Shared.Tools
{
    public class RateLimiter
    {
        private readonly int quota;
        private int remaining;
        private int skipped;

        public RateLimiter(int quota)
        {
            this.quota = quota;
            remaining = quota;
        }

        public int Reset()
        {
            var skippedBefore = skipped;
            remaining = quota;
            return skippedBefore;
        }

        public bool Check()
        {
            if (remaining == 0)
            {
                skipped++;
                return false;
            }

            remaining--;
            return true;
        }
    }
}