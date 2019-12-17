namespace MyScore.Pack.GamePack
{
    public class GameUrl : BaseUrl
    {
        public string GameId { get; set; }

        public string Fixture { get; set; }

        public override string Prefix { get; protected set; } = "match/{0}/{1}";

        protected override string[] Organize() => new string[] { GameId, Fixture };
    }
}
