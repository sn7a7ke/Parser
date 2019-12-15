using System.Collections.Generic;

namespace Parser.MyScore.Models.Football
{
    public class Game
    {
        public GameSummary Summary { get; set; }

        public List<GameIncident> Incidents { get; set; }
    }
}
