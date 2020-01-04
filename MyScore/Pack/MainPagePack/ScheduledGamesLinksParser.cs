using MyScore.Pack.CommonPack;

namespace MyScore.Pack.MainPagePack
{
    public class ScheduledGamesLinksParser : GamesLinksParser
    {
        public ScheduledGamesLinksParser() : base(Constants.MatchScheduledClass, Constants.EndOfMyLeaguesClass)
        {
        }
    }
}
