using HtmlAgilityPack;
using Parser;
using System;
using System.Collections.Generic;

namespace MyScore.Pack.CommonPack
{
    public class AttributesByPatternParser : Parser<List<string>>
    {
        public string XPath { get; set; } = "";

        public Func<HtmlNode, bool> IsEnd { get; set; } = (n) => false;

        public Func<HtmlNode, string> GetDesired { get; set; } = (n) => null;

        public override List<string> Parse()
        {
            var results = new List<string>();
            var children = Document.DocumentNode.SelectSingleNode(XPath)?.ChildNodes;
            if (children == null)
                return results;
            foreach (var node in children)
            {
                if (IsEnd(node))
                    break;
                var res = GetDesired(node);
                if (!string.IsNullOrEmpty(res))
                    results.Add(res);
            }
            return results;
        }
    }
}
