using System;
using System.Collections.Generic;
using System.Text;

namespace Happy
{
    public class QuoteRss
    {
        public string quoteText{get; set;}
        public override string ToString()
        {
            return this.quoteText;
        }
    }
}
