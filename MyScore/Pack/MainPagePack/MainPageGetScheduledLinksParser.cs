using MyScore.Pack.CommonPack;
using Parser;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MyScore.Pack.MainPagePack
{
    public class MainPageGetScheduledLinksParser : Parser<List<string>>
    {
        public override List<string> Parse()
        {
            var parser = new AttributesByPatternParser
            {
                XPath = XPath.MainPageLiveTable,
                IsEnd = n => n.ContainClass(Constants.EndOfMyLeaguesClass),
                GetDesired = n =>
                {
                    var attribute = n.Attributes["id"]?.Value ?? "";
                    if (Regex.IsMatch(attribute, Constants.GameAttributePattern) && n.ContainClass(Constants.MatchScheduledClass))
                        return attribute;
                    return "";
                },
                Document = this.Document
            };
            var results = parser.Parse().Select(s => s.Substring(4)).ToList();
            return results;
        }
    }
}
