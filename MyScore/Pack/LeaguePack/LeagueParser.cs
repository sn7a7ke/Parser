using Parser;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MyScore.Pack.LeaguePack
{
    public class LeagueParser : Parser<List<string>>
    {
        public override List<string> Parse()
        {
            var results = new List<string>();
            var parentNode = Document.DocumentNode.SelectSingleNode("//*[@id=\"live-table\"]/div[1]/div/div");
            var rgx = new Regex(@"^g_1_\w+");
            foreach (var node in parentNode.ChildNodes)
            {
                var id = node.Attributes["id"]?.Value;                
                if (!string.IsNullOrEmpty(id) && rgx.IsMatch(id))
                    results.Add(id.Substring(4));
            }
            return results;            
        }
    }
}
