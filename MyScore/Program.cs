using MyScore.Pack.GamePack;
using MyScore.Pack.LeaguePack;
using Parser.Interfaces;
using System;

namespace MyScore
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var leagueDetails = new LeagueDetails
            {
                Game = "football",
                Country = "ukraine",
                League = "premier-league-2017-2018",
                Fixture = "results"
            };
            var leagueExecutor = new LeagueExecutor();
            var gameLinks = DoParsing(leagueExecutor, leagueDetails);

            var gameDetails = new GameDetails
            {
                GameId = "n3eCfNzq", //"vquzagAh"
                Fixture = "#match-summary"
            };
            var gameExecutor = new GameExecutor();
            var game = DoParsing(gameExecutor, gameDetails);

            Console.WriteLine("Done...");
            Console.ReadKey();
        }

        private static TResult DoParsing<TDetails, TResult>(IExecutor<TDetails, TResult> executor, TDetails details)
        {            
            return executor.Run(details);
        }
    }
}
