using HtmlAgilityPack;

namespace Parser.Interfaces
{
    public interface IExecutor
    {
        HtmlDocument Document { get; set; }

        void Load(IUrl url, string pendingXPath = null);

        T Parse<T>(IParser<T> parser);

        T Process<T>(IUrl url, IParser<T> parser, string pendingXPath = null);
    }
}