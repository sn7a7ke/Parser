using MyScore.Pack.CommonPack;

namespace MyScore.Pack.MainPagePack
{
    public class MainPageGetLiveLinksParser : GameLinksParser
    {
        public MainPageGetLiveLinksParser() : base(Constants.MatchLiveClass, Constants.EndOfMyLeaguesClass)
        {
        }
    }
}
