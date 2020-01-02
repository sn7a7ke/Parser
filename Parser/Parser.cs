using HtmlAgilityPack;
using Parser.Interfaces;
using System;

namespace Parser
{
    public abstract class Parser<T> : IParser<T>
    {
        private string _xPath = "";

        public HtmlDocument Document { get; set; }

        public string XPath
        {
            get => _xPath;
            set => _xPath = value ?? throw new NullReferenceException();
        }

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
