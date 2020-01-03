using HtmlAgilityPack;
using Parser;

namespace MyScore.Pack.MainPagePack
{
    public class MainPageGetLiveLinksParser : ListParser<string>
    {
        public MainPageGetLiveLinksParser() : base(XPathConstants.LiveTable)
        {
        }

        public override string GetDesired(HtmlNode node) => node.Attribute("id", AttributePatternConstants.GameCode, Constants.MatchLiveClass);

        public override bool IsEnd(HtmlNode node) => node.ContainClass(Constants.EndOfMyLeaguesClass);
    }
}
