using HtmlAgilityPack;
using MyScore.Models.Football;
using Parser;

namespace MyScore.Pack.LeaguePack
{
    public class LeagueParser : Parser<League>
    {
        public LeagueParser() : base("//div[@class=\"table__body\"]/child::div[contains(@class,\"table__row\")]")
        {
        }

        public override League Parse()
        {
            var league = LeagueSummaryParse();

            var ltParser = new LeagueTeamParser(this.XPath)
            {
                Document = this.Document
            };
            var teams = ltParser.Parse();

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

        public class LeagueTeamParser : ListParser<TeamResults>
        {
            public LeagueTeamParser(string xPath) : base(xPath)
            {
            }

            public override TeamResults GetDesired(HtmlNode node)
            {
                var team = TeamParse(node.XPath);
                var tParser = new TeamFormParser(node.XPath + "//div[contains(@class,\"table__cell--form\")]/child::a[not(contains(@class,\"form__cell--upcoming\"))]")
                {
                    Document = this.Document
                };
                team.Forms = tParser.Parse();
                return team;
            }

            private TeamResults TeamParse(string xPath)
            {
                var team = new TeamResults();

                var attr = AttributeSplit(xPath + "//span[contains(@class,\"team_name_span\")]/a", "onclick", '/');
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
        }

        public class TeamFormParser : ListParser<TeamForm>
        {
            public TeamFormParser(string xPath) : base(xPath)
            {
            }

            public override TeamForm GetDesired(HtmlNode node) => TeamFormParse(node.XPath);

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
}
