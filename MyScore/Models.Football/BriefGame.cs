﻿namespace MyScore.Models.Football
{
    public class BriefGame
    {
        public string Code { get; set; }

        public string Country { get; set; }

        public string League { get; set; }

        public string LeagueId { get; set; }

        // Only one of both
        public string Stage { get; set; }
        public string Time { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public string ScoreHomeTeam { get; set; }

        public string ScoreAwayTeam { get; set; }

        public string ScoreHalfHomeTeam { get; set; }

        public string ScoreHalfAwayTeam { get; set; }

        public string ScoreFTHomeTeam { get; set; }

        public string ScoreFTAwayTeam { get; set; }
    }
}
