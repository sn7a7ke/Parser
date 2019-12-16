using HtmlAgilityPack;

namespace Parser.Interfaces
{
    public interface IParser<out T>
    {
        T Parse(HtmlDocument document);
    }
}
