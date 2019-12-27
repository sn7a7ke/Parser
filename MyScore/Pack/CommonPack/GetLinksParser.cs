using Parser;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MyScore.Pack.CommonPack
{
    public class GetLinksParser : Parser<List<string>>
    {
        public override List<string> Parse()
        {
            var parser = new AttributesByPatternParser
            {
                XPath = XPath.LeaguePageLiveTable,
                GetDesired = n =>
                {
                    var attribute = n.Attributes["id"]?.Value ?? "";
                    if (Regex.IsMatch(attribute, Constants.GameAttributePattern))
                        return attribute;
                    return "";
                },
                Document = this.Document
            };
            var results = parser.Parse();
            return results;
        }
    }
}
