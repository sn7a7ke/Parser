using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace Parser
{
    public class ListParser<T> : Parser<List<T>>
    {
        private Func<HtmlNode, T> _getDesired = (n) => default(T);
        private Func<HtmlNode, bool> _isEnd = (n) => false;
        private string _xPath;

        public string XPath
        {
            get => _xPath;
            set => _xPath = value ?? throw new NullReferenceException();
        }

        public Func<HtmlNode, T> GetDesired
        {
            get => _getDesired;
            set => _getDesired = value ?? throw new NullReferenceException();
        }

        public Func<HtmlNode, bool> IsEnd
        {
            get => _isEnd;
            set => _isEnd = value ?? throw new NullReferenceException();
        }

        public override List<T> Parse()
        {
            var results = new List<T>();
            var targetNodes = GetNodes(XPath);
            if (targetNodes == null)
                return results;
            foreach (var node in targetNodes)
            {
                if (IsEnd(node))
                    break;
                var res = GetDesired(node);
                if (res != null)
                    results.Add(res);
            }
            return results;
        }
    }
}
