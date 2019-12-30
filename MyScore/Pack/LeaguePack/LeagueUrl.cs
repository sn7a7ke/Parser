namespace MyScore.Pack.LeaguePack
{
    public class LeagueUrl : BaseUrl
    {
        public string Game { get; set; }

        public string Country { get; set; } = "";

        public string League { get; set; } = "";

        public string Fixture { get; set; } = "results";

        public override string[] Chunks() => new string[] { Game, Country, League, Fixture };
    }
}
