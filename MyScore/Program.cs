using MyScore.Models.Football;
using MyScore.Pack.GamePack;
using MyScore.Pack.LeaguePack;
using Parser;
using Parser.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private static void Executor_ReceivedChunk(object obj, IEnumerable<string> data)
        {
            foreach (var item in data)
                Console.WriteLine($"{item}");

            var file = new Storage("ukraine_2017-2018.txt");
            file.Save(data);
        }

        private static void Executor_Done(object obj)
        {
            Console.WriteLine("Done...");
            Console.ReadKey();
        }
    }
}
