using MyScore.Pack.CommonPack;

namespace MyScore.Pack.MainPagePack
{
    public class LiveGamesLinksParser : GamesLinksParser
    {
        public LiveGamesLinksParser() : base(Constants.MatchLiveClass, true)
        {
        }
    }
}
