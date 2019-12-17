using HtmlAgilityPack;
using Parser.Interfaces;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MyScore.Pack.LeaguePack
{
    public class LeagueParser : IParser<IEnumerable<string>>
    {
        public IEnumerable<string> Parse(HtmlDocument document)
        {
            var results = new List<string>();
            var parentNode = document.DocumentNode.SelectSingleNode("//*[@id=\"live-table\"]/div[1]/div/div");
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
