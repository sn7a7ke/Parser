using HtmlAgilityPack;
using Parser.Interfaces;
using System;

namespace Parser
{
    public abstract class Parser<T> : IParser<T>
    {
        public HtmlDocument Document { get; set; }

        public abstract T Parse();

        public string InnerText(string xPath)
        {
            return InnerText(xPath, s => s);
        }

        public string InnerText(string xPath, Func<string, string> func)
        {
            var node = Document.DocumentNode.SelectSingleNode(xPath);
            return func(node?.InnerText)?.Trim();
        }
    }
}
