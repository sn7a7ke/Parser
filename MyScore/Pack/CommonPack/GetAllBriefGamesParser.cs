using MyScore.Models.Football;
using Parser;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MyScore.Pack.CommonPack
{
    public class GetAllBriefGamesParser : Parser<List<BriefGame>>
    {
        public override List<BriefGame> Parse()
        {
            BriefGame brief;
            var results = new List<BriefGame>();
            string country = null;
            string league = null;
            var rgx = new Regex(@"^\d+_\d+_");
            string leagueId = null;
            var targetNodes = GetNodes(XPathConstants.LiveTable);
            if (targetNodes == null)
                return results;
            foreach (var node in targetNodes)
            {
                if (node.HasClass("event__header"))
                {
                    country = node.InnerTextByClass("event__title--type");
                    league = node.InnerTextByClass("event__title--name");
                    var tempNode = node.SelectSingleNode("//span[contains(@class,\"toggleMyLeague\")]");
                    var classes = tempNode?.GetClasses();
                    leagueId = classes?.FirstOrDefault(a => rgx.IsMatch(a));
                    brief = null;
                }
                else
                {
                    brief = BriefGameParse(node.XPath);
                    brief.Country = country;
                    brief.League = league;
                    brief.LeagueId = leagueId;
                }
                if (brief != null)
                    results.Add(brief);
            }
            return results;
        }

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
