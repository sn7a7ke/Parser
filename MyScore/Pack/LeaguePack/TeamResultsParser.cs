using HtmlAgilityPack;
using MyScore.Models.Football;
using Parser;

namespace MyScore.Pack.LeaguePack
{
    public class TeamResultsParser : ListParser<TeamResult>
    {
        private readonly TeamFormsParser _tfParser;

        public override HtmlDocument Document
        {
            get => base.Document;
            set
            {
                base.Document = value;
                _tfParser.Document = value;
            }
        }

        public TeamResultsParser(string xPath = null)
        {
            DescendantPrefix = "./child::div[contains(@class,\"table__row\")]";
            XPath = (xPath ?? "") + "//div[@class=\"table__body\"]";
            AddPending();
            _tfParser = new TeamFormsParser();
            Pending.AddRange(_tfParser.Pending);
        }

        public override TeamResult GetDesired(HtmlNode node)
        {
            var team = TeamParse(node);
            _tfParser.XPath = node.SelectSingleNode(".//div[contains(@class,\"table__cell--col_form\")]").XPath;
            team.Forms = _tfParser.Parse();
            return team;
        }

        private TeamResult TeamParse(HtmlNode node)
        {
            var team = new TeamResult();

            var attr = node.SelectSingleNode(".//span[contains(@class,\"team_name_span\")]/a")?.AttributeSplit("onclick", '/');
            if (attr?.Length >= 4)
            {
                team.InnerName = attr[2];
                team.Code = attr[3];
            }

            team.Name = node.DescendantInnerText(".//span[contains(@class,\"team_name_span\")]/a");

            team.Matches = node.DescendantInnerText(".//div[contains(@class,\"table__cell--matches_played\")]");

            team.Wins = node.DescendantInnerText(".//div[contains(@class,\"table__cell--wins_regular\")]");

            team.Draws = node.DescendantInnerText(".//div[contains(@class,\"table__cell--draws\")]");

            team.Losses = node.DescendantInnerText(".//div[contains(@class,\"table__cell--losses_regular\")]");

            team.GoalsScored = node.SelectSingleNode(".//div[contains(@class,\"table__cell--goals\")]")?.InnerTextSplit(0, ':');

            team.GoalsConceded = node.SelectSingleNode(".//div[contains(@class,\"table__cell--goals\")]")?.InnerTextSplit(1, ':');

            team.Points = node.DescendantInnerText(".//div[contains(@class,\"table__cell--points\")]");

            return team;
        }
    }
}
