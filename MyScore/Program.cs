using MyScore.Pack.CommonPack;
using MyScore.Pack.GamePack;
using MyScore.Pack.LeaguePack;
using MyScore.Pack.MainPagePack;
using MyScore.Pack.TeamPack;
using Parser;
using Parser.Interfaces;
using System;
using System.Collections.Generic;

namespace MyScore
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var selenium = new SeleniumLoader();
            var cacheLoader = new LoaderCacheProxy(selenium);
            Executor.Loader = cacheLoader;
            IExecutor executor = new Executor();

            IUrl mainUrl = new MainPageUrl();
            var myLeaguesLinks = executor.Process(mainUrl, new MyLeaguesParser());
            var deficit = Utility.MissingElements(myLeaguesLinks, Const.MyLeaguesPrefix);

            #region Action
            //var mpAction = new MainPageAction(selenium);
            //mpAction.RemoveLeagues(new List<string> { "1_77_KIShoMk3" });
            //mpAction.AddLeagues(deficit);
            //mpAction.Yesterday();
            //var briefResult2 = executor.Parse(new BriefGamesParser());
            #endregion

            var scheduledGames = executor.Parse(new ScheduledGamesLinksParser());

            var liveGames = executor.Parse(new LiveGamesLinksParser());

            var briefResult = executor.Parse(new BriefGamesParser());

            IUrl leagueUrl = new LeagueUrl
            {
                Game = "football",
                Country = "ukraine",
                League = "premier-league-2017-2018",
            };
            var gameLinks = executor.Process(leagueUrl, new AllGamesLinksParser());
            //"Показать больше матчей" - CLICK

            IUrl leagueUrl2 = new LeagueUrl
            {
                Game = "football",
                Country = "ukraine",
                League = "premier-league",
                Fixture = "standings"
            };
            var league = executor.Process(leagueUrl2, new LeagueParser());

            IUrl gameUrl = new GameUrl
            {
                GameId = "S0yP1iaC" //lbdY67Wj
            };
            var game = executor.Process(gameUrl, new GameParser());

            IUrl gameUrl2 = new GameUrl
            {
                GameId = "EezrvIMP",
                Fixture = "#match-statistics;0"
            };
            var gameStatistic = executor.Process(gameUrl2, new GameStatisticParser());

            IUrl teamUrl = new TeamUrl
            {
                InnerName = "shakhtar",
                Code = "4ENWX2OA"
            };
            var team = executor.Process(teamUrl, new TeamParser());

            Console.WriteLine("Done...");
            Console.ReadKey();
        }
    }
}
