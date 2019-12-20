using System;
using PuppeteerSharp;

namespace CrawlerCore
{
    public class Crawler
    {
        public Crawler()
        {
            Console.WriteLine("Download Browser...");
            _ = new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision).Result;
            Console.WriteLine("Fim do download Browser!");
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
                }
            }
        }
    }
}
