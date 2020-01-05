using HtmlAgilityPack;
using MyScore.Models.Football;
using Parser;

namespace MyScore.Pack.GamePack
{
    public class GameStatisticParser : ListParser<GameStatistic>
    {
        public GameStatisticParser(string xPath = null)
        {
            XPath = (xPath ?? "") + "//div[@id=\"tab-statistics-0-statistic\"]";
            AddPending();
        }

        public override GameStatistic GetDesired(HtmlNode node)
        {
            var res = new GameStatistic();

            res.HomeTeamValue = node.DescendantInnerText(".//div[contains(@class,\"statText--homeValue\")]");

            res.Name = node.DescendantInnerText(".//div[contains(@class,\"statText--titleValue\")]");

            res.AwayTeamValue = node.DescendantInnerText(".//div[contains(@class,\"statText--awayValue\")]");

            return res;
        }
    }
}
