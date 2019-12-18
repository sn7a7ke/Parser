using Parser;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MyScore.Pack.MainPagePack
{
    public class MainPageMyLeaguesParser : Parser<List<string>>
    {
        public override List<string> Parse()
        {
            var results = new List<string>();
            var parentNode = Document.DocumentNode.SelectSingleNode("//*[@id=\"my-leagues-list\"]");
            var descs = parentNode.Descendants("a");
            foreach (var node in descs)
            {
                var href = node.Attributes["href"]?.Value;
                if (!string.IsNullOrEmpty(href) && href.Contains("football"))
                    results.Add(href);
            }
            return results;
        }
    }
}
