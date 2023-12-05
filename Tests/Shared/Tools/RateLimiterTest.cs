using NUnit.Framework;
using Shared.Tools;

namespace Tests.Shared.Tools
{
    namespace ClientPlugin.Tests
    {
        [TestFixture]
        public class RateLimiterTests
        {
            [Test]
            public void RateLimiter_RateLimit()
            {
                var rateLimiter = new RateLimiter(3);

                for (var i = 0; i < 3; i++)
                {
                    Assert.True(rateLimiter.Check());
                }

                Assert.False(rateLimiter.Check());
                Assert.AreEqual(1, rateLimiter.Reset());
                Assert.True(rateLimiter.Check());
            }
        }
    }
}