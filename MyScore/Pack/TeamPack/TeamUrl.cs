namespace MyScore.Pack.TeamPack
{
    public class TeamUrl : BaseUrl
    {
        public string InnerName { get; set; }

        public string Code { get; set; }

        public string Fixture { get; set; } = "";

        public override string[] Chunks() => new string[] { "team", InnerName, Code, Fixture };
    }
}
