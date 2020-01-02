using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace Parser
{
    public class ListParser<T> : Parser<List<T>>
    {
        private Func<HtmlNode, T> _getDesired = (n) => default(T);
        private Predicate<HtmlNode> _isHeader = (n) => false;
        private Func<HtmlNode, T> _getHeader = (n) => default(T);
        private Func<T, T, T> _filling = (t1, t2) => t2;
        private Func<HtmlNode, bool> _isEnd = (n) => false;

        public Func<T, T, T> Filling
        {
            get => _filling;
            set => _filling = value ?? throw new NullReferenceException();
        }

        public Predicate<HtmlNode> IsHeader
        {
            get => _isHeader;
            set => _isHeader = value ?? throw new NullReferenceException();
        }

        public Func<HtmlNode, T> GetHeader
        {
            get => _getHeader;
            set => _getHeader = value ?? throw new NullReferenceException();
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
            T header = default(T);
            foreach (var node in targetNodes)
            {
                if (IsEnd(node))
                    break;
                if (IsHeader(node))
                    header = GetHeader(node);
                else
                {
                    var res = GetDesired(node);
                    if (res != null)
                        results.Add(res);
                    res = Filling(header, res);
                    results.Add(res);
                }
            }
            return results;
        }
    }
}
