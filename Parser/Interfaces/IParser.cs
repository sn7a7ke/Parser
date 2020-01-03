using HtmlAgilityPack;

namespace Parser.Interfaces
{
    public interface IParser<out T>
    {
        HtmlDocument Document { get; set; }

        string XPath { get; }

        T Parse();
    }
}
