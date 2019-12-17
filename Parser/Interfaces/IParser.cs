using HtmlAgilityPack;
using System;

namespace Parser.Interfaces
{
    public interface IParser<out T>
    {
        HtmlDocument Document { get; set; }

        T Parse();

        string InnerTextNode(string xPath);

        string InnerTextNode(string xPath, Func<string, string> func);
    }
}
