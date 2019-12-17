namespace MyScore.Pack.LeaguePack
{
    public class LeagueUrl : BaseUrl<LeagueDetails>
    {
        public override string Prefix { get; protected set; } = "{0}/{1}/{2}/{3}/";

        public override string Get(LeagueDetails details)
        {
            var url = string.Format(Template, details.Game, details.Country, details.League, details.Fixture);
            return url;
        }
    }
}
