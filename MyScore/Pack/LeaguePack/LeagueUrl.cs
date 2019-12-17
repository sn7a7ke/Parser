namespace MyScore.Pack.LeaguePack
{
    public class LeagueUrl : BaseUrl
    {
        public string Game { get; set; }

        public string Country { get; set; }

        public string League { get; set; }

        public string Fixture { get; set; }
        
        public override string Prefix { get; protected set; } = "{0}/{1}/{2}/{3}/";

        protected override string[] Organize() => new string[] { Game, Country, League, Fixture };
    }
}
