using HtmlAgilityPack;
using Parser.Interfaces;

namespace Parser
{
    public class Executor : IExecutor
    {
        public HtmlDocument Document => Loader.Document;

        public static ILoader Loader { get; set; }

        public virtual void Load(IUrl url, string pendingXPath = null) => Loader.GetPage(url, pendingXPath);

        public virtual T Parse<T>(IParser<T> parser)
        {
            if (!string.IsNullOrEmpty(parser.XPath) && !Loader.WaitEnabledElement(parser.XPath))
                throw new NodeNotFoundException();
            parser.Document = Document;
            var results = parser.Parse();
            return results;
        }

        public virtual T Process<T>(IUrl url, IParser<T> parser, string pendingXPath = null)
        {
            var pending = (string.IsNullOrEmpty(pendingXPath) && !string.IsNullOrEmpty(parser.XPath)) ? parser.XPath : null;
            Load(url, pending);
            var results = Parse(parser);
            return results;
        }
    }
}
