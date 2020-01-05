using HtmlAgilityPack;
using MyScore.Models.Football;
using MyScore.Pack.CommonPack;
using Parser;

namespace MyScore.Pack.TeamPack
{
    public class TeamParser : Parser<Team>
    {
        private readonly BriefGamesParser _lastParser;
        private readonly BriefGamesParser _scheduleParser;

        public override HtmlDocument Document
        {
            get => base.Document;
            set
            {
                base.Document = value;
                _lastParser.Document = value;
                _scheduleParser.Document = value;
            }
        }

        public TeamParser(string xPath = null)
        {
            XPath = (xPath ?? "") + "//div[@id=\"mc\"]";
            _lastParser = new BriefGamesParser("//div[@id=\"live-table\"]//div[contains(@class,\"summary-results\")]");
            Pending.AddRange(_lastParser.Pending);
            _scheduleParser = new BriefGamesParser("//div[@id=\"live-table\"]//div[contains(@class,\"summary-fixtures\")]");
            Pending.AddRange(_scheduleParser.Pending);
        }

        public override Team Parse()
        {
            var team = TeamSummaryParse();
            team.LastResults = _lastParser.Parse();
            team.Schedule = _scheduleParser.Parse();
            return team;
        }

        private Team TeamSummaryParse()
        {
            var sum = new Team();

            sum.Name = Node.DescendantInnerText(".//div[@class=\"teamHeader__name\"]");

            var parts = Node.SelectSingleNode(".//*[@id=\"li0\"]")?.AttributeSplit("href", '/');
            if (parts?.Length >= 4)
            {
                sum.InnerName = parts[2];
                sum.Code = parts[3];
            }

            sum.CountryCode = Node.SelectSingleNode(".//h2[contains(@class,\"tournament\")]//span[contains(@class,\"flag\")]")?.AttributeExactlyPattern("class", AttributePatternConstants.CountryCode);

            return sum;
        }
    }
}
