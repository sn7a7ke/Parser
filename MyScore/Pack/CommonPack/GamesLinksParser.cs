using HtmlAgilityPack;
using Parser;

namespace MyScore.Pack.CommonPack
{
    public class GamesLinksParser : ListParser<string>
    {
        private readonly string _constraint;
        private readonly string _ending;

        public GamesLinksParser(string constraint, string ending, string xPath = null)
        {
            _constraint = constraint;
            _ending = ending;
            XPath = (xPath ?? "") + "//div[contains(@class,\"sportName\")]";
            AddPending();
        }

        public override string GetDesired(HtmlNode node) => node.Attribute("id", AttributePatternConstants.GameCode, _constraint ?? "");

        public override bool IsEnd(HtmlNode node)
        {
            return string.IsNullOrEmpty(_ending) ? base.IsEnd(node) : node.ContainClass(Constants.EndOfMyLeaguesClass);
        }
    }
}
