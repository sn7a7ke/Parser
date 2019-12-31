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
            return func(GetNode(xPath)?.InnerText)?.Trim();
        }

        public string InnerTextSplit(string xPath, int choice, params char[] separator)
        {
            return GetNode(xPath)?.InnerTextSplit(choice, separator);
        }

        public HtmlNode GetNode(string xPath) => Document.DocumentNode.SelectSingleNode(xPath);

        public HtmlNodeCollection GetNodes(string xPath) => Document.DocumentNode.SelectNodes(xPath);
    }
}
