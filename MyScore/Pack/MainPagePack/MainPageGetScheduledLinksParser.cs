using HtmlAgilityPack;
using Parser;

namespace MyScore.Pack.MainPagePack
{
    public class MainPageGetScheduledLinksParser : ListParser<string>
    {
        public MainPageGetScheduledLinksParser() : base(XPathConstants.LiveTable)
        {
        }

        public override string GetDesired(HtmlNode node) => node.Attribute("id", AttributePatternConstants.GameCode, Constants.MatchScheduledClass);

        public override bool IsEnd(HtmlNode node) => node.ContainClass(Constants.EndOfMyLeaguesClass);
    }
}
