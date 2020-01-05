using HtmlAgilityPack;
using System.Collections.Generic;

namespace Parser.Interfaces
{
    public interface IParser<out T>
    {
        HtmlDocument Document { get; set; }

        HtmlNode Node { get; }

        string XPath { get; set; }

        List<string> Pending { get; }

        T Parse();
    }
}
