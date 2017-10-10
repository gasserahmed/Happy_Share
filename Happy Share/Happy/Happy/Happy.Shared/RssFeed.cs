using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Syndication;

namespace Happy
{
    class RssFeed
    {
        public async static Task< List<QuoteRss> > addItem()
        {
            List<QuoteRss> quoteList=new List<QuoteRss>();
            SyndicationClient Client = new SyndicationClient();
            Uri RSSUri = new Uri("http://happyquotes.tumblr.com/rss");
            var feeds = await Client.RetrieveFeedAsync(RSSUri);
            foreach (var feed in feeds.Items)
            {
                if (feed.Title.Text.StartsWith("\"") & !feed.Title.Text.Contains("..."))
                    quoteList.Add(new QuoteRss { quoteText = feed.Title.Text.Substring(0, feed.Title.Text.Length - 2)+"\"" });
            }
           Uri RSSUri1 = new Uri("http://www.inspirationline.com/inspirationline.xml");
            var feeds1 = await Client.RetrieveFeedAsync(RSSUri1);
            foreach (var feed in feeds1.Items)
            {
                 if (!feed.Summary.Text.StartsWith("&"))
                    quoteList.Add(new QuoteRss { quoteText = "\"" + feed.Summary.Text.Substring(0, feed.Summary.Text.Length - 3) + "\"" });
            }
            return quoteList;
        }
    }
}
