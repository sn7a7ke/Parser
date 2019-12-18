using MyScore.Pack.GamePack;
using MyScore.Pack.LeaguePack;
using MyScore.Pack.MainPagePack;
using Parser;
using Parser.Interfaces;
using System;

namespace MyScore
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Executor.Loader = new SeleniumLoader();
            var executor = new Executor();

            IUrl mainUrl = new MainPageUrl();
            var myLeaguesLinks = executor.Process(mainUrl, new MainPageMyLeaguesParser());

            IUrl leagueUrl = new LeagueUrl
            {
                Game = "football",
                Country = "ukraine",
                League = "premier-league-2017-2018",
                Fixture = "results"
            };
            var gameLinks = executor.Process(leagueUrl, new LeagueParser());

            IUrl gameUrl = new GameUrl
            {
                GameId = "n3eCfNzq",
                Fixture = "#match-summary"
            };
            var game = executor.Process(gameUrl, new GameParser());

            Console.WriteLine("Done...");
            Console.ReadKey();
        }
    }
}
