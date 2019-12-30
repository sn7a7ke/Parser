using Parser;

namespace MyScore.Pack.CommonPack
{
    public class GetLinksParser : ListParser<string>
    {
        public GetLinksParser()
        {
            XPath = XPathConstants.LiveTable;
            GetDesired = n => n.Attribute("id", AttributePatternConstants.GameCode);
        }
    }
}
