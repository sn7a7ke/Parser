using MyScore.Models.Football;
using Parser;
using System.Collections.Generic;

namespace MyScore.Pack.LeaguePack
{
    public class LeagueGetLeagueParser : Parser<League>
    {
        public override League Parse()
        {
            var league = LeagueSummaryParse();

            var teams = TeamsParse("//div[@class=\"table__body\"]/child::div[contains(@class,\"table__row\")]");
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

            league.Code = Document.DocumentNode.SelectSingleNode("//div[@id=\"tomyleagues\"]//span[contains(@class,\"toggleMyLeague\")]")?.AttributeExactlyPattern("class", AttributePatternConstants.LeagueCode);

            league.Country = InnerText("//h2[contains(@class,\"tournament\")]//span[contains(@class,\"flag\")]/following-sibling::a");

            league.CountryCode = Document.DocumentNode.SelectSingleNode("//h2[contains(@class,\"tournament\")]//span[contains(@class,\"flag\")]")?.AttributeExactlyPattern("class", AttributePatternConstants.CountryCode);

            return league;
        }

        private List<LeagueTeam> TeamsParse(string xPath, bool includeForm = true)
        {
            var teamNodes = Document.DocumentNode.SelectNodes(xPath);
            var teams = new List<LeagueTeam>();
            LeagueTeam team;
            foreach (var node in teamNodes)
            {
                team = TeamParse(node.XPath);
                if (includeForm)
                    team.Forms = TeamFormsParse(node.XPath + "//div[contains(@class,\"table__cell--form\")]/child::a[not(contains(@class,\"form__cell--upcoming\"))]");
                teams.Add(team);
            }
            return teams;
        }

        private LeagueTeam TeamParse(string xPath)
        {
            var team = new LeagueTeam();

            var attr = Document.DocumentNode.SelectSingleNode(xPath + "//span[contains(@class,\"team_name_span\")]/a")?.GetAttributeValue("onclick", null)?.Split('/');
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

        private List<TeamForm> TeamFormsParse(string xPath)
        {
            var formNodes = Document.DocumentNode.SelectNodes(xPath);
            var forms = new List<TeamForm>();
            foreach (var n in formNodes)
                forms.Add(TeamFormParse(n.XPath));
            return forms;
        }

        private TeamForm TeamFormParse(string xPath)
        {
            var form = new TeamForm();

            var title = Document.DocumentNode.SelectSingleNode(xPath)?.GetAttributeValue("title", null);
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
