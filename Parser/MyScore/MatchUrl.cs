namespace Parser.MyScore
{
    public class MatchUrl : BaseUrl, IUrl<MatchDetails>
    {
        public virtual string Prefix { get; protected set; } = "{matchId}/{fixture}";

        public virtual string Get(MatchDetails details)
        {
            var url = string.Format($"{Base}{Prefix}", details.MatchId, details.Fixture);
            return url;
        }
    }
}
