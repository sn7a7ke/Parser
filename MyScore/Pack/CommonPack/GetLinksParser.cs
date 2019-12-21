using Parser;
using System.Collections.Generic;

namespace MyScore.Pack.CommonPack
{
    public class GetLinksParser : Parser<List<string>>
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
            var results = parser.Parse();
            return results;
        }
    }
}
