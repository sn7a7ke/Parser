using System.Collections.Generic;

namespace MyScore.Models.Football
{
    public class Game
    {
        public GameSummary Summary { get; set; }

        public List<GameIncident> Incidents { get; set; }

        public List<GameStatistic> Statistics { get; set; }
    }
}
