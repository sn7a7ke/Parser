using HtmlAgilityPack;

namespace Parser.Interfaces
{
    public interface ILoader
    {
        HtmlDocument Document { get; }

        HtmlDocument GetPage(IUrl url, string pendingXPath = null);
    }
}