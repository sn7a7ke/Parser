namespace MyScore.Models.Football
{
    public class GameSummary
    {
        public string Code { get; set; }

        public string Country { get; set; }

        public string League { get; set; }

        public string Round { get; set; }

        public string DateTime { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public string ScoreHomeTeam { get; set; }

        public string ScoreAwayTeam { get; set; }

        //before a penalty shootout
        public string ScoreFTHomeTeam { get; set; }

        public string ScoreFTAwayTeam { get; set; }

        public string Completed { get; set; }

        public string ScoreHalfHomeTeam { get; set; }

        public string ScoreHalfAwayTeam { get; set; }

        public string Judge { get; set; }

        public string Attendance { get; set; }

        public string Stadium { get; set; }
    }
}
