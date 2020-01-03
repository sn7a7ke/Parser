using HtmlAgilityPack;
using Parser;

namespace MyScore.Pack.MainPagePack
{
    public class MainPageGetMyLeaguesParser : ListParser<string>
    {
        public MainPageGetMyLeaguesParser() : base(XPathConstants.MyLeaguesList)
        {
        }

        public override string GetDesired(HtmlNode node) => node.AttributeExactlyPattern("class", AttributePatternConstants.LeagueCode);
    }
}
