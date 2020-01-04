using HtmlAgilityPack;
using MyScore.Models.Football;
using Parser;
using System.Linq;
using System.Text.RegularExpressions;

namespace MyScore.Pack.CommonPack
{
    public class BriefGamesParser : ListParser<BriefGame>
    {
        public BriefGamesParser() : base(XPathConstants.LiveTable)
        {
        }

        public BriefGamesParser(string xPath) : base(xPath + XPathConstants.LiveTable)
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

            res.Code = GetNode(xPath).GetAttributeValue("id", null);

            res.Stage = InnerText(xPath + "//div[contains(@class,\"event__stage--block\")]");

            res.Time = GetNode(xPath + "//div[contains(@class,\"event__time\")]")?.GetDirectInnerText()?.Trim();

            res.HomeTeam = InnerText(xPath + "//div[contains(@class,\"event__participant--home\")]");

            res.AwayTeam = InnerText(xPath + "//div[contains(@class,\"event__participant--away\")]");

            res.ScoreHomeTeam = InnerText(xPath + "//div[contains(@class,\"event__scores\")]/span");

            res.ScoreAwayTeam = InnerText(xPath + "//div[contains(@class,\"event__scores\")]/span[last()]");

            var innerFT = InnerText(xPath + "//div[contains(@class,\"event__scores\")]/div[@class=\"event__part\"]");
            if (innerFT != null)
            {
                var rgx = new Regex(@"\d+");
                var halvesScore = rgx.Matches(innerFT);
                if (halvesScore.Count >= 2)
                {
                    res.ScoreFTHomeTeam = halvesScore[0].Value;
                    res.ScoreFTAwayTeam = halvesScore[1].Value;
                }
            }

            var innerHalf = InnerText(xPath + "/div[@class=\"event__part\"]");
            if (innerHalf != null)
            {
                var rgx = new Regex(@"\d+");
                var halvesScore = rgx.Matches(innerHalf);
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
