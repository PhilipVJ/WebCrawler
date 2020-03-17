using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace WebCrawler_1
{
    public class CrawlAgent
    {
        private CrawlManager manager;

        public CrawlAgent(CrawlManager manager)
        {
            this.manager = manager;
        }

        public void StartCrawling(CancellationToken token, string searchWord)
        {
            while (!token.IsCancellationRequested)
            {
                List<Link> links = new List<Link>();
                var link = manager.GetLink();

                //No links available. Wait a second and try again
                if (link == null)
                {
                    Thread.Sleep(1000);
                    continue;
                }
                UriBuilder ub = new UriBuilder(link.URL);

                //Get URLS
                HtmlWeb hw = new HtmlWeb();
                hw.UserAgent = "SchoolProjectAgent";

                HtmlDocument doc = null;
                try
                {
                    doc = hw.Load(ub.Uri.ToString());
                }
                catch
                {
                    this.manager.AddToVisitedLink(link.URL, false);
                    continue;
                }

                if (doc.DocumentNode.InnerText.Contains(searchWord))
                {
                    this.manager.AddToSearchResultView(link.URL);
                }
                // Add to visited pages
                this.manager.AddToVisitedLink(link.URL, true);

                // Check if there are any href tags
                if(doc.DocumentNode.SelectNodes("//a[@href]") == null)
                {
                    continue;
                }

                foreach (HtmlNode linkHere in doc.DocumentNode.SelectNodes("//a[@href]"))
                {
                    HtmlAttribute att = linkHere.Attributes["href"];
                    if (att.Value.Contains("http://") || att.Value.Contains("https://"))
                    {
                        links.Add(new Link { URL = att.Value, Depth = link.Depth + 1 });
                    }
                }

                //Add to queue
                this.manager.AddLinks(links);
            }
        }
    }
}
