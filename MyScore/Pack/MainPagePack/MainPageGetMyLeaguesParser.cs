using Parser;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MyScore.Pack.MainPagePack
{
    public class MainPageGetMyLeaguesParser : Parser<List<string>>
    {
        public override List<string> Parse()
        {
            var results = new List<string>();
            var targetNodes = Document.DocumentNode.SelectNodes("//*[@id=\"my-leagues-list\"]//descendant::a");
            foreach (var node in targetNodes)
            {
                var href = node.Attributes["href"]?.Value;
                if (!string.IsNullOrEmpty(href) && href.Contains("football"))
                    results.Add(href);
            }
            return results;
        }
    }
}
