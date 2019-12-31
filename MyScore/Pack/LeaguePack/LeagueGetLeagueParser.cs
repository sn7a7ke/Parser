using MyScore.Models.Football;
using Parser;

namespace MyScore.Pack.LeaguePack
{
    public class LeagueGetLeagueParser : Parser<League>
    {
        public override League Parse()
        {
            var league = LeagueSummaryParse();

            var teams = new ListParser<LeagueTeam>
            {
                XPath = "//div[@class=\"table__body\"]/child::div[contains(@class,\"table__row\")]",
                GetDesired = n =>
                {
                    var team = TeamParse(n.XPath);
                    team.Forms = new ListParser<TeamForm>
                    {
                        XPath = n.XPath + "//div[contains(@class,\"table__cell--form\")]/child::a[not(contains(@class,\"form__cell--upcoming\"))]",
                        GetDesired = nn => TeamFormParse(nn.XPath),
                        Document = this.Document
                    }.Parse();
                    return team;
                },
                Document = this.Document
            }.Parse();

            foreach (var t in teams)
            {
                t.LeagueCode = league.Code;
                foreach (var f in t.Forms)
                {
                    f.TeamCode = t.Code;
                    f.LeagueCode = t.LeagueCode;
                }
            }
            league.Teams = teams;

            return league;
        }

        private League LeagueSummaryParse()
        {
            var league = new League();

            league.Name = InnerText("//div[@id=\"fscon\"]//div[contains(@class,\"teamHeader__name\")]");

            league.Code = GetNode("//div[@id=\"tomyleagues\"]//span[contains(@class,\"toggleMyLeague\")]")?.AttributeExactlyPattern("class", AttributePatternConstants.LeagueCode);

            league.Country = InnerText("//h2[contains(@class,\"tournament\")]//span[contains(@class,\"flag\")]/following-sibling::a");

            league.CountryCode = GetNode("//h2[contains(@class,\"tournament\")]//span[contains(@class,\"flag\")]")?.AttributeExactlyPattern("class", AttributePatternConstants.CountryCode);

            return league;
        }

        private LeagueTeam TeamParse(string xPath)
        {
            var team = new LeagueTeam();

            var attr = GetNode(xPath + "//span[contains(@class,\"team_name_span\")]/a")?.GetAttributeValue("onclick", null)?.Split('/');
            if (attr?.Length >= 4)
            {
                team.InnerName = attr[2];
                team.Code = attr[3];
            }

            team.Name = InnerText(xPath + "//span[contains(@class,\"team_name_span\")]/a");

            team.Matches = InnerText(xPath + "//div[contains(@class,\"table__cell--matches_played\")]");

            team.Wins = InnerText(xPath + "//div[contains(@class,\"table__cell--wins_regular\")]");

            team.Draws = InnerText(xPath + "//div[contains(@class,\"table__cell--draws\")]");

            team.Losses = InnerText(xPath + "//div[contains(@class,\"table__cell--losses_regular\")]");

            team.GoalsScored = InnerTextSplit(xPath + "//div[contains(@class,\"table__cell--goals\")]", 0, ':');

            team.GoalsConceded = InnerTextSplit(xPath + "//div[contains(@class,\"table__cell--goals\")]", 1, ':');

            team.Points = InnerText(xPath + "//div[contains(@class,\"table__cell--points\")]");

            return team;
        }

        private TeamForm TeamFormParse(string xPath)
        {
            var form = new TeamForm();

            var title = GetNode(xPath)?.GetAttributeValue("title", null);
            var score = title?.Split(']', ':', '&');
            if (score?.Length >= 3)
            {
                form.ScoreHomeTeam = score[1].Trim();
                form.ScoreAwayTeam = score[2].Trim();
            }

            var team = title?.Split('(', '-', ')');
            if (team?.Length >= 4)
            {
                form.HomeTeam = team[1].Trim();
                form.AwayTeam = team[2].Trim();
                form.DateTime = team[3].Trim();
            }

            return form;
        }
    }
}
