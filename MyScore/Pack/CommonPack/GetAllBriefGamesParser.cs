using HtmlAgilityPack;
using MyScore.Models.Football;
using Parser;
using System.Linq;
using System.Text.RegularExpressions;

namespace MyScore.Pack.CommonPack
{
    public class GetAllBriefGamesParser : ListParser<BriefGame>
    {
        public GetAllBriefGamesParser() : base(XPathConstants.LiveTable)
        {
        }

        public override bool IsHeader(HtmlNode node) => node.HasClass("event__header");

        public override BriefGame Filling(BriefGame header, BriefGame processed)
        {
            processed.Country = header.Country;
            processed.League = header.League;
            processed.LeagueId = header.LeagueId;
            return processed;
        }

        public override BriefGame GetHeader(HtmlNode node)
        {
            BriefGame header = new BriefGame();
            header.Country = node.InnerTextByClass("event__title--type");
            header.League = node.InnerTextByClass("event__title--name");
            var tempNode = node.SelectSingleNode("//span[contains(@class,\"toggleMyLeague\")]");
            var classes = tempNode?.GetClasses();
            header.LeagueId = classes?.FirstOrDefault(a => Regex.IsMatch(a, AttributePatternConstants.LeagueCode));
            return header;
        }

        public override BriefGame GetDesired(HtmlNode node) => BriefGameParse(node.XPath);

        private BriefGame BriefGameParse(string xPath)
        {
            var res = new BriefGame();

            res.Link = GetNode(xPath).GetAttributeValue("id", null);

            res.Stage = InnerText(xPath + "//div[contains(@class,\"event__stage--block\")]");

            res.Time = InnerText(xPath + "//div[contains(@class,\"event__time\")]");

            res.HomeTeam = InnerText(xPath + "//div[contains(@class,\"event__participant--home\")]");

            res.AwayTeam = InnerText(xPath + "//div[contains(@class,\"event__participant--away\")]");

            res.ScoreHomeTeam = InnerText(xPath + "//div[contains(@class,\"event__scores\")]/span");

            res.ScoreAwayTeam = InnerText(xPath + "//div[contains(@class,\"event__scores\")]/span[last()]");

            var inner = InnerText(xPath + "//div[@class=\"event__part\"]");
            if (inner != null)
            {
                var rgx = new Regex(@"\d+");
                var halvesScore = rgx.Matches(inner);
                if (halvesScore.Count >= 2)
                {
                    res.ScoreHalfHomeTeam = halvesScore[0].Value;
                    res.ScoreHalfAwayTeam = halvesScore[1].Value;
                }
            }

            return res;
        }
    }
}
