using HtmlAgilityPack;
using Parser;

namespace MyScore.Pack.CommonPack
{
    public class GamesLinksParser : ListParser<string>
    {
        private readonly string _constraint;
        private readonly bool _myLeagues;

        public GamesLinksParser(string constraint, bool myLeagues, string xPath = null)
        {
            _constraint = constraint;
            _myLeagues = myLeagues;
            XPath = (xPath ?? "") + XPathConst.ContainsSportName;
            AddPending();
        }

        public override string GetDesired(HtmlNode node) => node.Attribute("id", AttrPatternConst.GameCode, _constraint ?? "");

        public override bool IsEnd(HtmlNode node)
        {
            return _myLeagues ? (node.ContainClass(ClassConst.EventHeader) && !node.ContainClass("top")) : base.IsEnd(node);
        }
    }
}
