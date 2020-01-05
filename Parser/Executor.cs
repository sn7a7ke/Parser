using HtmlAgilityPack;
using Parser.Interfaces;

namespace Parser
{
    public class Executor : IExecutor
    {
        public HtmlDocument Document => Loader.Document;

        public static ILoader Loader { get; set; }

        public virtual void Load(IUrl url, string pendingXPath = null)
        {
            Loader.GetPage(url, pendingXPath);
        }

        public virtual T Parse<T>(IParser<T> parser)
        {
            if (!ToWait(parser))
                throw new NodeNotFoundException();
            parser.Document = Document;
            var results = parser.Parse();
            return results;
        }

        public virtual T Process<T>(IUrl url, IParser<T> parser, string pendingXPath = null)
        {
            Load(url, pendingXPath);
            return Parse(parser);
        }

        private bool ToWait<T>(IParser<T> parser)
        {
            foreach (var p in parser.Pending)
                if (!Loader.WaitEnabledElement(p))
                    return false;
            return true;
        }
    }
}
