using HtmlAgilityPack;

namespace Parser
{
    public interface IParser<out T>
    {
        T Parse(HtmlDocument document);
    }
}
