using MyScore.Pack.GamePack;
using MyScore.Pack.LeaguePack;
using Parser;
using Parser.Interfaces;
using System;

namespace MyScore
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            ILoader loader = new SeleniumLoader();

            IUrl leagueUrl = new LeagueUrl
            {
                Game = "football",
                Country = "ukraine",
                League = "premier-league-2017-2018",
                Fixture = "results"
            };
            var leagueExecutor = new LeagueExecutor(loader);
            var gameLinks = leagueExecutor.Run(leagueUrl);


            IUrl gameUrl = new GameUrl
            {
                GameId = "n3eCfNzq", //"vquzagAh"
                Fixture = "#match-summary"
            };
            var gameExecutor = new GameExecutor(loader);
            var game = gameExecutor.Run(gameUrl);


            Console.WriteLine("Done...");
            Console.ReadKey();
        }
    }
}
