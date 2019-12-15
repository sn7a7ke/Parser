using HtmlAgilityPack;
using System.Collections.Generic;

namespace Parser
{
    public interface IParser<out T>
    {
        IEnumerable<T> Parse(HtmlDocument document);
    }
}
