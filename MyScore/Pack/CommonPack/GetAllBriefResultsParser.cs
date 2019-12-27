using MyScore.Models.Football;
using Parser;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MyScore.Pack.CommonPack
{
    public class GetAllBriefResultsParser : Parser<List<BriefResult>>
    {
        public override List<BriefResult> Parse()
        {
            var briefParser = new GetBriefResultParser();
            briefParser.Document = Document;
            BriefResult brief;
            var results = new List<BriefResult>();
            string country = null;
            string league = null;
            var rgx = new Regex(@"^\d+_\d+_");
            string leagueId = null;
            var parentNode = Document.DocumentNode.SelectSingleNode(XPath.MainPageLiveTable);
            foreach (var node in parentNode.ChildNodes)
            {
                if (node.HasClass("event__header"))
                {
                    country = node.InnerTextByClass("event__title--type");
                    league = node.InnerTextByClass("event__title--name");
                    var tempNode = node.SelectSingleNode("//span[contains(@class,\"toggleMyLeague\")]");
                    var classes = tempNode?.GetClasses();
                    leagueId = classes?.FirstOrDefault(a => rgx.IsMatch(a));
                    brief = null;
                }
                else
                {
                    briefParser.Id = node.Attributes["id"].Value;
                    brief = briefParser.Parse();
                    brief.Country = country;
                    brief.League = league;
                    brief.LeagueId = leagueId;
                }

                if (brief != null)
                    results.Add(brief);
            }
            return results;
        }
    }
}
