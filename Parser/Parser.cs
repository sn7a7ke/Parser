using HtmlAgilityPack;
using Parser.Interfaces;
using System.Collections.Generic;

namespace Parser
{
    public abstract class Parser<T> : IParser<T>
    {
        public virtual HtmlDocument Document { get; set; }

        public virtual HtmlNode Node => Document.DocumentNode.SelectSingleNode(XPath);

        public virtual string XPath { get;  set; } = "//body";

        public virtual List<string> Pending { get; protected set; } = new List<string>();

        public abstract T Parse();
    }
}
