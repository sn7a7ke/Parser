namespace MyScore.Pack.GamePack
{
    public class GameUrl : BaseUrl<GameDetails>
    {
        public override string Prefix { get; protected set; } = "match/{0}/{1}";

        public override string Get(GameDetails details)
        {
            var url = string.Format(Template, details.GameId, details.Fixture);
            return url;
        }
    }
}
