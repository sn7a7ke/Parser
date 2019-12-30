using MyScore.Pack.CommonPack;
using MyScore.Pack.GamePack;
using MyScore.Pack.LeaguePack;
using MyScore.Pack.MainPagePack;
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
            Executor.Loader = selenium;
            var executor = new Executor();

            IUrl mainUrl = new MainPageUrl();
            var myLeaguesLinks = executor.Process(mainUrl, new MainPageGetMyLeaguesParser(), XPathConstants.WaitingMainPageMyLeague);
            var deficit = Utility.MissingElements(myLeaguesLinks, Constants.MyLeaguesPrefix);
            var mpAction = new MainPageAction(selenium);
            mpAction.RemoveLeagues(new List<string> { "1_77_KIShoMk3" });
            mpAction.AddLeagues(deficit);

            var scheduledGames = executor.Parse(new MainPageGetScheduledLinksParser());
            var liveGames = executor.Parse(new MainPageGetLiveLinksParser());
            var briefResult = executor.Parse(new GetAllBriefGamesParser());

            IUrl leagueUrl = new LeagueUrl
            {
                Game = "football",
                Country = "ukraine",
                League = "premier-league-2017-2018",
            };
            var gameLinks = executor.Process(leagueUrl, new GetLinksParser());

            IUrl leagueUrl2 = new LeagueUrl
            {
                Game = "football",
                Country = "ukraine",
                League = "premier-league",
                Fixture = "standings"
            };
            var league = executor.Process(leagueUrl2, new LeagueGetLeagueParser(), XPathConstants.WaitingLeaguePageTable);

            IUrl gameUrl = new GameUrl
            {
                GameId = "S0yP1iaC"
            };
            var game = executor.Process(gameUrl, new GameGetGameParser());

            Console.WriteLine("Done...");
            Console.ReadKey();
        }
    }
}
