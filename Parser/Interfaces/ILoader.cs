using HtmlAgilityPack;

namespace Parser.Interfaces
{
    public interface ILoader<in T>
    {
        HtmlDocument GetPage(T details);

        HtmlDocument GetPage(T details, string pendingXPath);
    }
}