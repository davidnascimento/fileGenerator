using System;
using Xunit;

namespace Test.ByteCounter
{
    public class ByteCounterTest
    {
        [Fact]
        public void ByteCounterMothereff()
        {
            var byteCounter = new ByteCounterCrawler.ByteCounter();
            var count = byteCounter.Count("Desd");

            Assert.Equal(4, count);
        }

        [Fact]
        public void ByteCounter()
        {
            var byteCounter = new ByteCounterCrawler.ByteCounter("", "");
            var count = byteCounter.Count("Desde");

            Assert.Equal(5, count);
        }
    }
}
