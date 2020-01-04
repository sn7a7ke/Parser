using HtmlAgilityPack;
using MyScore.Models.Football;
using Parser;

namespace MyScore.Pack.GamePack
{
    public class GameStatisticParser : ListParser<GameStatistic>
    {
        public GameStatisticParser() : base("//div[@id=\"tab-statistics-0-statistic\"]/child::div")
        {
        }

        public override GameStatistic GetDesired(HtmlNode node) => ParameterGameStatisticParse(node.XPath);

        private GameStatistic ParameterGameStatisticParse(string xPath)
        {
            var res = new GameStatistic();

            res.HomeTeamValue = InnerText(xPath + "//div[contains(@class,\"statText--homeValue\")]");

            res.Name = InnerText(xPath + "//div[contains(@class,\"statText--titleValue\")]");

            res.AwayTeamValue = InnerText(xPath + "//div[contains(@class,\"statText--awayValue\")]");

            return res;
        }
    }
}
