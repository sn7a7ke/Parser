using Parser;
using Parser.Interfaces;
using System.Collections.Generic;

namespace MyScore.Pack.LeaguePack
{
    public class LeagueExecutor : Executor<IEnumerable<string>>
    {
        public LeagueExecutor(ILoader loader) : base(loader, new LeagueParser())
        {
        }
    }
}
