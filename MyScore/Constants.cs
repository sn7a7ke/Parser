using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MyScore
{
    public static class Const
    {
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

    public static class ClassConst
    {
        public const string MatchScheduled = "event__match--scheduled";

        public const string MatchLive = "event__match--live";

        public const string EventHeader = "event__header";

        public const string SportName = "sportName";
    }

    public static class XPathConst
    {
        public const string ContainsBegin = "//div[contains(@class,\"";

        public const string ContainsEnd = "\")]";

        public const string ContainsSportName = ContainsBegin + ClassConst.SportName + ContainsEnd;

        public const string ContainsEventHeader = ContainsBegin + ClassConst.EventHeader + ContainsEnd;

        public const string IdMyLeaguesList = "//*[@id=\"my-leagues-list\"]";
    }

    public static class AttrPatternConst
    {
        public const string GameCode = @"^g_1_\w+";

        public const string TeamCode = @"\d+_[\w\d]+";

        public const string LeagueCode = @"\d+_\d+_[\w\d]+";

        public const string CountryCode = @"fl_\d+";
    }
}
