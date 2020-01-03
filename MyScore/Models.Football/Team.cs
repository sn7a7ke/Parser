using System.Collections.Generic;

namespace MyScore.Models.Football
{
    public class Team
    {
        public string Code { get; set; }

        public string InnerName { get; set; }

        public string Name { get; set; }

        public string CountryCode { get; set; }

        public List<BriefGame> LastResults { get; set; }

        public List<BriefGame> Schedule { get; set; }
    }
}
