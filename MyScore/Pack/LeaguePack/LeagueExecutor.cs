using Parser;
using System.Collections.Generic;

namespace MyScore.Pack.LeaguePack
{
    public class LeagueExecutor : AExecutor<LeagueDetails, IEnumerable<string>>
    {
        public LeagueExecutor()
        {
            _parser = new LeagueDetailsParser();
            var url = new LeagueUrl();
            _loader = new SeleniumLoader<LeagueDetails>(url);
        }
    }
}
