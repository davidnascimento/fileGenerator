using System;
using PuppeteerSharp;

namespace CrawlerCore
{
    public class Crawler
    {
        public Crawler()
        {
            _ = new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision).Result;
        }

        protected internal T ExecuteJsCommand<T>(string url, string jsCommand)
        {
            var options = new LaunchOptions { Headless = true };
            using (var browser = Puppeteer.LaunchAsync(options).Result)
            {
                using (var page = browser.NewPageAsync().Result)
                {
                    _ = page.GoToAsync(url).Result;
                    
                    return page.EvaluateExpressionAsync<T>(jsCommand).Result;
                    //var jsSelector = @"document.querySelector('#bytes').innerText";
                    //Int32.TryParse(Regex.Match(innerText, @"\d+").Value, out int count);
                }
            }
        }
    }
}
