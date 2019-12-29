using Parser;

namespace MyScore.Pack.MainPagePack
{
    public class MainPageGetLiveLinksParser : ListParser<string>
    {
        public MainPageGetLiveLinksParser()
        {
            XPath = XPathConstants.LiveTable;
            IsEnd = n => n.ContainClass(Constants.EndOfMyLeaguesClass);
            GetDesired = n => n.Attribute("id", Constants.GameAttributePattern, Constants.MatchLiveClass);
        }
    }
}
