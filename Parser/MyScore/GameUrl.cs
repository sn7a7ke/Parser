using Parser.Interfaces;

namespace Parser.MyScore
{
    public class GameUrl : BaseUrl, IUrl<GameDetails>
    {
        public virtual string Prefix { get; protected set; } = "match/{0}/{1}";

        public virtual string Get(GameDetails details)
        {
            var url = string.Format($"{Base}{Prefix}", details.GameId, details.Fixture);
            return url;
        }
    }
}
