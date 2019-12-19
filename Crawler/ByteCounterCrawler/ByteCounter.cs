using System;
using System.Text;
using System.Text.RegularExpressions;
using CrawlerCore;
using Polly;

namespace ByteCounterCrawler
{
    public class ByteCounter : Crawler
    {
        private string _url = "https://mothereff.in/byte-counter#";
        private string _jsSelector = @"document.querySelector('#bytes').innerText";

        public ByteCounter()
        {
        }

        public ByteCounter(string url, string jsSelector)
        {
            _url = url;
            _jsSelector = jsSelector;
        }

        public int Count(string text)
        {
            var policyFallbackForAnyException =
                Policy<int>
                .Handle<Exception>()
                .OrResult(r => r <= 0)
                .Fallback(() => CountBytesLocal(text));

            return policyFallbackForAnyException.Execute(() => CountBytesApi(text));
        }

        private int CountBytesApi(string text)
        {
            var innerText = ExecuteJsCommand<string>(_url + text, _jsSelector);
            Int32.TryParse(Regex.Match(innerText, @"\d+").Value, out int count);

            return count;
        }

        private static int CountBytesLocal(string text)
        {
            return Encoding.UTF8.GetByteCount(text);
        }
    }
}
