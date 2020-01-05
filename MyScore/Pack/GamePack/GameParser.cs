using HtmlAgilityPack;
using MyScore.Models.Football;
using Parser;

namespace MyScore.Pack.GamePack
{
    public class GameParser : Parser<Game>
    {
        private readonly GameIncidentsParser _giParser;

        public override HtmlDocument Document
        {
            get => base.Document;
            set
            {
                base.Document = value;
                _giParser.Document = value;
            }
        }

        public GameParser(string xPath = null)
        {
            XPath = (xPath ?? "") + "//div[@id=\"detcon\"]";
            _giParser = new GameIncidentsParser();
            Pending.AddRange(_giParser.Pending);
        }

        public override Game Parse()
        {
            var game = GameSummaryParse();
            game.Incidents = _giParser.Parse();
            return game;
        }

        private Game GameSummaryParse()
        {
            var sum = new Game();

            var descNode = Node.SelectSingleNode(".//div[@class=\"description\"]");

            sum.Country = descNode?.SelectSingleNode(".//span[@class=\"description__country\"]").InnerTextSplit(0, ':');

            var league = descNode?.DescendantInnerText(".//a[@href=\"#\"]").Split('-');
            if (league?.Length >= 2)
            {
                sum.League = league[0].Trim();
                sum.Round = league[1].Trim();
            }

            sum.DateTime = descNode?.DescendantInnerText(".//*[@id=\"utime\"]");

            var headNode = Node.SelectSingleNode(".//div[@class=\"team-primary-content\"]");

            sum.HomeTeam = headNode?.DescendantInnerText(".//div[contains(@class,\"tname-home\")]//a[@href=\"#\"]");

            sum.AwayTeam = headNode?.DescendantInnerText(".//div[contains(@class,\"tname-away\")]//a[@href=\"#\"]");

            var score = headNode?.DescendantInnerText(".//*[@id=\"event_detail_current_result\"]")?.Split('-', '(', ')');
            if (score?.Length >= 2)
            {
                sum.ScoreHomeTeam = score[0].Trim();
                sum.ScoreAwayTeam = score[1].Trim();
            }
            if (score?.Length >= 4)
            {
                sum.ScoreFTHomeTeam = score[2].Trim();
                sum.ScoreFTAwayTeam = score[3].Trim();
            }

            sum.Completed = headNode?.DescendantInnerText(".//div[contains(@class,\"info-status\")]");

            sum.ScoreHalfHomeTeam = Node.DescendantInnerText(".//*[@id=\"summary-content\"]//div[contains(@class,\"stage-12\")]//span[@class=\"p1_home\"]");

            sum.ScoreHalfAwayTeam = Node.DescendantInnerText(".//*[@id=\"summary-content\"]//div[contains(@class,\"stage-12\")]//span[@class=\"p1_away\"]");

            var infoNode = Node.SelectSingleNode(".//div[@class=\"match-information-data\"]");

            sum.Judge = infoNode?.DescendantInnerTextByContent("Судья")?.Split(':', ',')[1];

            sum.Attendance = infoNode?.DescendantInnerTextByContent("Посещаемость")?.Split(':', ',')[1];

            sum.Stadium = infoNode?.DescendantInnerTextByContent("Стадион")?.Split(':', ',')[1];

            return sum;
        }
    }
}
