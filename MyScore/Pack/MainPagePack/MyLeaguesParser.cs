using HtmlAgilityPack;
using Parser;

namespace MyScore.Pack.MainPagePack
{
    public class MyLeaguesParser : ListParser<string>
    {
        public MyLeaguesParser() : base(XPathConstants.MyLeaguesList)
        {
        }

        public override string GetDesired(HtmlNode node) => node.AttributeExactlyPattern("class", AttributePatternConstants.LeagueCode);
    }
}
