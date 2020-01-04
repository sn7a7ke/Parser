using MyScore.Pack.CommonPack;

namespace MyScore.Pack.MainPagePack
{
    public class MainPageGetScheduledLinksParser : GameLinksParser
    {
        public MainPageGetScheduledLinksParser() : base(Constants.MatchScheduledClass, Constants.EndOfMyLeaguesClass)
        {
        }
    }
}
