using HtmlAgilityPack;
using MyScore.Models.Football;
using Parser;

namespace MyScore.Pack.LeaguePack
{
    public class TeamFormsParser : ListParser<TeamForm>
    {
        public TeamFormsParser(string xPath = null)
        {
            DescendantPrefix = "./child::a[not(contains(@class,\"form__cell--upcoming\"))]";
            XPath = (xPath ?? "") + "//div[contains(@class,\"table__cell--form\")]";
            AddPending();
        }

        public override TeamForm GetDesired(HtmlNode node)
        {
            var form = new TeamForm();

            var title = node?.GetAttributeValue("title", null);
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
