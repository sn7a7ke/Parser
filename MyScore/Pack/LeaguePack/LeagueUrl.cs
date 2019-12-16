using Parser.Interfaces;

namespace MyScore.Pack.LeaguePack
{
    public class LeagueUrl : BaseUrl, IUrl<LeagueDetails>
    {
        public virtual string Prefix { get; protected set; } = "{0}/{1}/{2}/{3}/";

        public virtual string Get(LeagueDetails details)
        {
            var template = $"{Base}{Prefix}";

            var url = string.Format(template, details.Game, details.Country, details.League, details.Fixture);
            return url;
        }
    }
}
