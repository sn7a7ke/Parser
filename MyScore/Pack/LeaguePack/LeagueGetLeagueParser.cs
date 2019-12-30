using MyScore.Models.Football;
using Parser;
using System.Collections.Generic;

namespace MyScore.Pack.LeaguePack
{
    public class LeagueGetLeagueParser : Parser<League>
    {
        public override League Parse()
        {
            var league = new League();

            league.Name = InnerText("//div[@id=\"fscon\"]//div[contains(@class,\"teamHeader__name\")]");

            league.Code = Document.DocumentNode.SelectSingleNode("//div[@id=\"tomyleagues\"]//span[contains(@class,\"toggleMyLeague\")]").AttributeExactlyPattern("class", AttributePatternConstants.LeagueCode);

            league.Country = InnerText("//h2[contains(@class,\"tournament\")]//span[contains(@class,\"flag\")]/following-sibling::a");

            league.CountryCode = Document.DocumentNode.SelectSingleNode("//h2[contains(@class,\"tournament\")]//span[contains(@class,\"flag\")]").AttributeExactlyPattern("class", AttributePatternConstants.CountryCode);

            var teamNodes = Document.DocumentNode.SelectNodes("//div[@class=\"table__body\"]/child::div[contains(@class,\"table__row\")]");
            var teams = new List<Team>();
            Team team;
            foreach (var node in teamNodes)
            {
                team = TeamParse(node.XPath);
                team.LeagueCode = league.Code;
                teams.Add(team);
            }

            league.Teams = teams;

            return league;
        }

        private Team TeamParse(string xPath)
        {
            var team = new Team();

            var attr = Document.DocumentNode.SelectSingleNode(xPath + "//span[contains(@class,\"team_name_span\")]/a").GetAttributeValue("onclick", null)?.Split('/');
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
}
