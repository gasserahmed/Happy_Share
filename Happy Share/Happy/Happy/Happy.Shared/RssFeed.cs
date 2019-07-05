using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Syndication;

namespace Happy
{
    class RssFeed
    {
        public async static Task< List<QuoteRss> > addItem()
        {
            List<QuoteRss> quoteList = new List<QuoteRss>();
            try
            {
                SyndicationClient Client = new SyndicationClient();
                Uri RSSUri = new Uri("http://happyquotes.tumblr.com/rss");
                var feeds = await Client.RetrieveFeedAsync(RSSUri);
                foreach (var feed in feeds.Items)
                {
                    if (feed.Title.Text.StartsWith("\"") & !feed.Title.Text.Contains("..."))
                        quoteList.Add(new QuoteRss { quoteText = feed.Title.Text.Substring(0, feed.Title.Text.Length - 2) + "\"" });
                }
            }
            catch (Exception exception) 
            {
                Debug.WriteLine("Error while getting quotes: "+exception);
            }
            return quoteList;
        }
    }
}
