namespace MyScore
{
    public static class Constants
    {
        public const string GameAttributePattern = @"^g_1_\w+";

        public const string EndOfMyLeaguesClass = "event__header--no-my-games";

        public const string MatchScheduledClass = "event__match--scheduled";

        public const string MatchLiveClass = "event__match--live";
    }

    public static class XPath
    {
        public const string MainPageLiveTable = "//*[@id=\"live-table\"]/div[2]/div/div";

        public const string LeaguePageLiveTable = "//*[@id=\"live-table\"]/div[1]/div/div";

        public const string WaitingElement = "//*[@id=\"live-table\"]";
    }
}
