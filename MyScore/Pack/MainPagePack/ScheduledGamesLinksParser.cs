using MyScore.Pack.CommonPack;

namespace MyScore.Pack.MainPagePack
{
    public class ScheduledGamesLinksParser : GamesLinksParser
    {
        public ScheduledGamesLinksParser() : base(ClassConst.MatchScheduled, true)
        {
        }
    }
}
