using HtmlAgilityPack;
using System;

namespace Parser.Interfaces
{
    public interface IParser<out T>
    {
        HtmlDocument Document { get; set; }

        T Parse();

        string InnerText(string xPath);

        string InnerText(string xPath, Func<string, string> func);
    }
}
