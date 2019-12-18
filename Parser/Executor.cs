using HtmlAgilityPack;
using Parser.Interfaces;

namespace Parser
{
    public class Executor : IExecutor
    {
        public HtmlDocument Document { get; set; } = new HtmlDocument();

        public static ILoader Loader { get; set; }

        public virtual void Load(IUrl url)
        {
            Document = Loader.GetPage(url);
        }

        public virtual T Parse<T>(IParser<T> parser)
        {
            parser.Document = Document;
            var results = parser.Parse();
            return results;
        }

        public virtual T Process<T>(IUrl url, IParser<T> parser)
        {
            Load(url);
            var results = Parse(parser);
            return results;
        }
    }
}
