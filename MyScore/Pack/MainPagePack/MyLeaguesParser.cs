using HtmlAgilityPack;
using Parser;

namespace MyScore.Pack.MainPagePack
{
    public class MyLeaguesParser : ListParser<string>
    {
        public MyLeaguesParser(string xPath = null)
        {
            DescendantPrefix = ".//descendant::span";
            XPath = (xPath ?? "") + "//*[@id=\"my-leagues-list\"]";
            AddPending();
        }

        public override string GetDesired(HtmlNode node) => node.AttributeExactlyPattern("class", AttributePatternConstants.LeagueCode);
    }
}
