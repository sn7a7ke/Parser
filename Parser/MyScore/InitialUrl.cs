namespace Parser.MyScore
{
    public class InitialUrl : BaseUrl, IUrl<InitialDetails>
    {
        public virtual string Prefix { get; protected set; } = "{0}/{1}/{2}/{3}/";

        public virtual string Get(InitialDetails details)
        {
            var template = $"{Base}{Prefix}";

            var url = string.Format(template, details.Game, details.Country, details.League, details.Fixture);
            return url;
        }
    }
}
