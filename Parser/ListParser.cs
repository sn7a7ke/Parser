using HtmlAgilityPack;
using System.Collections.Generic;

namespace Parser
{
    public abstract class ListParser<T> : Parser<List<T>>
    {
        public virtual HtmlNodeCollection Nodes => Node.SelectNodes(DescendantPrefix);

        public virtual string DescendantPrefix { get; protected set; } = "./child::div";

        protected virtual void AddPending()
        {
            var prefix = DescendantPrefix[0] == '.'
                ? DescendantPrefix.Substring(1)
                : DescendantPrefix;
            Pending.Add(XPath + prefix);
        }

        public virtual bool IsHeader(HtmlNode node) => false;

        public virtual T GetHeader(HtmlNode node) => default(T);

        public virtual T GetDesired(HtmlNode node) => default(T);

        public virtual T Filling(T header, T processed) => processed;

        public virtual bool IsEnd(HtmlNode node) => false;

        public override List<T> Parse()
        {
            var results = new List<T>();
            if (Nodes == null)
                return results;
            T header = default(T);
            foreach (var node in Nodes)
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
