using HtmlAgilityPack;

namespace Parser.Interfaces
{
    public interface ILoader
    {
        HtmlDocument GetPage(IUrl url);

        HtmlDocument GetPage(IUrl url, string pendingXPath);
    }
}