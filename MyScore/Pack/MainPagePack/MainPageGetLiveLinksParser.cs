using Parser;
using System.Text.RegularExpressions;

namespace MyScore.Pack.MainPagePack
{
    public class MainPageGetLiveLinksParser : ListParser<string>
    {
        public MainPageGetLiveLinksParser()
        {
            XPath = XPathConstants.LiveTable;
            IsEnd = n => n.ContainClass(Constants.EndOfMyLeaguesClass);
            GetDesired = n =>
            {
                var attribute = n.Attributes["id"]?.Value;
                if (attribute != null && Regex.IsMatch(attribute, Constants.GameAttributePattern) && n.ContainClass(Constants.MatchLiveClass))
                    return attribute;
                return null;
            };
        }
    }
}
