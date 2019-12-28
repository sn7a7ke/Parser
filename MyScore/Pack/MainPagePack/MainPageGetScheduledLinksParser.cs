using Parser;
using System.Text.RegularExpressions;

namespace MyScore.Pack.MainPagePack
{
    public class MainPageGetScheduledLinksParser : ListParser<string>
    {
        public MainPageGetScheduledLinksParser()
        {
            XPath = XPathConstants.LiveTable;
            IsEnd = n => n.ContainClass(Constants.EndOfMyLeaguesClass);
            GetDesired = n =>
            {
                var attribute = n.Attributes["id"]?.Value;
                if (attribute != null && Regex.IsMatch(attribute, Constants.GameAttributePattern) && n.ContainClass(Constants.MatchScheduledClass))
                    return attribute;
                return null;
            };
        }
    }
}
