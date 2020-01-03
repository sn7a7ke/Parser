using HtmlAgilityPack;
using Parser.Interfaces;
using System;

namespace Parser
{
    public abstract class Parser<T> : IParser<T>
    {
        protected Parser(string xPath)
        {
            XPath = xPath ?? throw new ArgumentNullException(nameof(xPath));
        }

        public HtmlDocument Document { get; set; }

        public string XPath { get; protected set; }

        public abstract T Parse();

        public string InnerText(string xPath) => InnerText(xPath, s => s);

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

        public string[] AttributeSplit(string xPath, string attribute, params char[] separator)
        {
            return GetNode(xPath)?.GetAttributeValue(attribute, null)?.Split(separator);
        }
    }
}
