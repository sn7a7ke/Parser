using HtmlAgilityPack;

namespace Parser
{
    public interface ILoader<in T>
    {
        HtmlDocument GetPage(T details);
    }
}