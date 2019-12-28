using Parser;
using System.Text.RegularExpressions;

namespace MyScore.Pack.CommonPack
{
    public class GetLinksParser : ListParser<string>
    {
        public GetLinksParser()
        {
            XPath = XPathConstants.LiveTable;
            GetDesired = n =>
            {
                var attribute = n.Attributes["id"]?.Value;
                if (attribute != null && Regex.IsMatch(attribute, Constants.GameAttributePattern))
                    return attribute;
                return null;
            };
        }
    }
}
