using MyScore.Pack.CommonPack;
using Parser;
using System.Collections.Generic;
using System.Linq;

namespace MyScore.Pack.MainPagePack
{
    public class MainPageGetLiveLinksParser : Parser<List<string>>
    {
        public override List<string> Parse()
        {
            var parser = new AttributesByPatternParser
            {
                XPath = "//*[@id=\"live-table\"]/div[2]/div/div",
                AttributePattern = @"^g_1_\w+",
                Attribute = "id",
                ClassContains = "event__match--live",
                Document = this.Document
            };
            var results = parser.Parse().Select(s => s.Substring(4)).ToList();
            return results;
        }
    }
}
