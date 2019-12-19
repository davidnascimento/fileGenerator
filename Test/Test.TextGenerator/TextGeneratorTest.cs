using System;
using Xunit;

namespace Test.TextGenerator
{
    public class TextGeneratorTest
    {
        [Fact]
        public void GetTextLeroLero()
        {
            var textGenerator = new TextGeneratorCrawler.TextGenerator();
            var text = textGenerator.GetText();

            Assert.NotEqual(string.Empty, text);
        }

        [Fact]
        public void GetTextRandom()
        {
            var textGenerator = new TextGeneratorCrawler.TextGenerator("", "");
            var text = textGenerator.GetText();

            Assert.NotEqual(string.Empty, text);
        }
    }
}
