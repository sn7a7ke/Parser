using Parser;

namespace MyScore.Pack.MainPagePack
{
    public class MainPageGetMyLeaguesParser : ListParser<string>
    {
        public MainPageGetMyLeaguesParser()
        {
            XPath = XPathConstants.MyLeaguesList;
            GetDesired = n => n.AttributeExactlyPattern("class", AttributePatternConstants.LeagueCode);
        }
    }
}
