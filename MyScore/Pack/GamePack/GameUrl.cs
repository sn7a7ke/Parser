namespace MyScore.Pack.GamePack
{
    public class GameUrl : BaseUrl
    {
        public string GameId { get; set; }

        public string Fixture { get; set; } = "#match-summary";

        public override string[] Chunks() => new string[] { "match", GameId, Fixture };
    }
}
