using Parser;
using System.Text.RegularExpressions;

namespace MyScore.Pack.MainPagePack
{
    public class MainPageGetMyLeaguesParser : ListParser<string>
    {
        public MainPageGetMyLeaguesParser()
        {
            XPath = XPathConstants.MyLeaguesList;
            GetDesired = n =>
            {
                var attribute = n.Attributes["class"]?.Value;
                if (!string.IsNullOrEmpty(attribute))
                {
                    var res = Regex.Match(attribute, Constants.LeagueAttributePattern).Value;
                    if (!string.IsNullOrEmpty(res))
                        return res;
                }
                return null;
            };
        }
    }
}
