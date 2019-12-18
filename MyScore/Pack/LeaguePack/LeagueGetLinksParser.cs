using MyScore.Pack.CommonPack;
using Parser;
using System.Collections.Generic;
using System.Linq;

namespace MyScore.Pack.LeaguePack
{
    public class LeagueGetLinksParser : Parser<List<string>>
    {
        public override List<string> Parse()
        {
            var parser = new AttributesByPatternParser
            {
                XPath = "//*[@id=\"live-table\"]/div[1]/div/div",
                AttributePattern = @"^g_1_\w+",
                Attribute = "id",
                Document = this.Document
            };
            var results = parser.Parse().Select(s => s.Substring(4)).ToList();
            return results;
        }
    }
}
