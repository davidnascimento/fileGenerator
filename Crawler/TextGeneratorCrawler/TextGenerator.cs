using System;
using CrawlerCore;
using Polly;

namespace TextGeneratorCrawler
{
    public class TextGenerator : Crawler
    {
        private string _url = "https://lerolero.com";
        private string _jsSelector = @"document.querySelector('.sentence').innerText";

        public TextGenerator()
        {
        }

        public TextGenerator(string url, string jsSelector)
        {
            _url = url;
            _jsSelector = string.IsNullOrEmpty(jsSelector) ? _jsSelector : jsSelector;
        }

        public string GetText()
        {
            var policyFallbackForAnyException =
                Policy<string>
                .Handle<Exception>()
                .OrResult(r => string.IsNullOrWhiteSpace(r))
                .Fallback(() => GerarLeroLero());

            return policyFallbackForAnyException.Execute(() => GetTextApi());
        }

        private string GetTextApi()
        {
            return ExecuteJsCommand<string>(_url, _jsSelector);
        }

        private string GerarLeroLero()
        {
            return GeradorLeroLero.GenerateSentence();
        }
    }
}
