using HtmlAgilityPack;
using Parser;

namespace MyScore.Pack.CommonPack
{
    public class GetLinksParser : ListParser<string>
    {
        public GetLinksParser() : base(XPathConstants.LiveTable)
        {
        }

        public override string GetDesired(HtmlNode node) => node.Attribute("id", AttributePatternConstants.GameCode);
    }
}
