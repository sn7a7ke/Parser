using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MyScore
{
    public static class Constants
    {
        public const string EndOfMyLeaguesClass = "event__header--no-my-games";

        public const string MatchScheduledClass = "event__match--scheduled";

        public const string MatchLiveClass = "event__match--live";

        public static readonly IList<string> MyLeaguesPrefix =
            new ReadOnlyCollection<string>(new List<string>
            {
                "1_198_dYlOSQOD",
                "1_81_W6BOzpK2",
                "1_176_QVmLl54o",
                "1_98_COuk57Ci",
                "1_195_Myjs3vp6",
                "1_195_ShUKWHDG",
                "1_77_KIShoMk3",
                "1_6_xGrwqq16",
                "1_6_ClDjv3V5",
                "1_6_KQMVOQ0g",
                "1_8_lvUBR5F8"
            });
    }

    public static class XPathConstants
    {
        public const string LiveTable = "//div[contains(@class,\"sportName\")]/child::div";

        public const string MyLeaguesList = "//*[@id=\"my-leagues-list\"]//descendant::span";
    }

    public static class AttributePatternConstants
    {
        public const string GameCode = @"^g_1_\w+";

        public const string TeamCode = @"\d+_[\w\d]+";

        public const string LeagueCode = @"\d+_\d+_[\w\d]+";

        public const string CountryCode = @"fl_\d+";
    }
}
