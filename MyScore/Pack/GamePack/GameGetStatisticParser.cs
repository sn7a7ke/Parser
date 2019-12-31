using MyScore.Models.Football;
using Parser;

namespace MyScore.Pack.GamePack
{
    public class GameGetStatisticParser : ListParser<GameStatistic>
    {
        public GameGetStatisticParser()
        {
            XPath = "//div[@id=\"tab-statistics-0-statistic\"]/child::div";
            GetDesired = n => ParameterGameStatisticParse(n.XPath);
        }

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
