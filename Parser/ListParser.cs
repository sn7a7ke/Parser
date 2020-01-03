using HtmlAgilityPack;
using System.Collections.Generic;

namespace Parser
{
    public class ListParser<T> : Parser<List<T>>
    {
        public ListParser(string xPath) : base(xPath)
        {
        }

        public virtual bool IsHeader(HtmlNode node) => false;

        public virtual T GetHeader(HtmlNode node) => default(T);

        public virtual T GetDesired(HtmlNode node) => default(T);

        public virtual T Filling(T header, T processed) => processed;

        public virtual bool IsEnd(HtmlNode node) => false;

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
                        results.Add(Filling(header, res));
                }
            }
            return results;
        }
    }
}
