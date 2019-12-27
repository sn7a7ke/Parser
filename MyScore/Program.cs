using MyScore.Pack.CommonPack;
using MyScore.Pack.GamePack;
using MyScore.Pack.LeaguePack;
using MyScore.Pack.MainPagePack;
using Parser;
using Parser.Interfaces;
using System;
using System.Linq;

namespace MyScore
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Executor.Loader = new SeleniumLoader();
            var executor = new Executor();

            IUrl mainUrl = new MainPageUrl();
            var myLeaguesLinks = executor.Process(mainUrl, new MainPageGetMyLeaguesParser(), XPath.WaitingElement).Select(s => s.Substring(4)).ToList();
            var scheduledGames = executor.Parse(new MainPageGetScheduledLinksParser());
            var liveGames = executor.Parse(new MainPageGetLiveLinksParser());
            var briefResult = executor.Parse(new GetAllBriefResultsParser());

            IUrl leagueUrl = new LeagueUrl
            {
                Game = "football",
                Country = "ukraine",
                League = "premier-league-2017-2018",
                Fixture = "results"
            };
            var gameLinks = executor.Process(leagueUrl, new GetLinksParser());

            IUrl gameUrl = new GameUrl
            {
                GameId = "n3eCfNzq",
                Fixture = "#match-summary"
            };
            var game = executor.Process(gameUrl, new GameGetGameParser());

            Console.WriteLine("Done...");
            Console.ReadKey();
        }
    }
}
