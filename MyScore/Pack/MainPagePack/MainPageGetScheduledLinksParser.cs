using Parser;

namespace MyScore.Pack.MainPagePack
{
    public class MainPageGetScheduledLinksParser : ListParser<string>
    {
        public MainPageGetScheduledLinksParser()
        {
            XPath = XPathConstants.LiveTable;
            IsEnd = n => n.ContainClass(Constants.EndOfMyLeaguesClass);
            GetDesired = n => n.Attribute("id", AttributePatternConstants.GameCode, Constants.MatchScheduledClass);
        }
    }
}
