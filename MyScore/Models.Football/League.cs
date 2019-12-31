using System.Collections.Generic;

namespace MyScore.Models.Football
{
    public class League
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string CountryCode { get; set; }

        public List<LeagueTeam> Teams { get; set; }
    }
}
