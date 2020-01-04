using MyScore.Models.Football;
using MyScore.Pack.CommonPack;
using Parser;

namespace MyScore.Pack.TeamPack
{
    public class TeamParser : Parser<Team>
    {
        public TeamParser() : base(XPathConstants.LiveTable)
        {
        }

        public override Team Parse()
        {
            var team = TeamSummaryParse();

            var lastParser = new GetAllBriefGamesParser("//div[@id=\"live-table\"]//div[contains(@class,\"summary-results\")]");
            lastParser.Document = this.Document;
            team.LastResults = lastParser.Parse();

            var scheduleParser = new GetAllBriefGamesParser("//div[@id=\"live-table\"]//div[contains(@class,\"summary-fixtures\")]");
            scheduleParser.Document = this.Document;
            team.Schedule = scheduleParser.Parse();

            return team;
        }

        private Team TeamSummaryParse()
        {
            var sum = new Team();

            sum.Name = InnerText("//div[@class=\"teamHeader__name\"]");

            var parts = AttributeSplit("//*[@id=\"li0\"]", "href", '/');
            if (parts?.Length >= 4)
            {
                sum.InnerName = parts[2];
                sum.Code = parts[3];
            }

            sum.CountryCode = GetNode("//h2[contains(@class,\"tournament\")]//span[contains(@class,\"flag\")]")?.AttributeExactlyPattern("class", AttributePatternConstants.CountryCode);

            return sum;
        }
    }
}
