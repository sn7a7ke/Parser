using MyScore.Models.Football;
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
            ILoader loader = new SeleniumLoader();

            IUrl mainUrl = new MainPageUrl();
            var mainPageMyLeaguesExecutor = new Executor<List<string>>(loader, new MainPageMyLeaguesParser());
            var myLeaguesLinks = mainPageMyLeaguesExecutor.Run(mainUrl);

            IUrl leagueUrl = new LeagueUrl
            {
                Game = "football",
                Country = "ukraine",
                League = "premier-league-2017-2018",
                Fixture = "results"
            };
            var leagueExecutor = new Executor<List<string>>(loader, new LeagueParser());
            var gameLinks = leagueExecutor.Run(leagueUrl);

            IUrl gameUrl = new GameUrl
            {
                GameId = "n3eCfNzq",
                Fixture = "#match-summary"
            };
            var gameExecutor = new Executor<Game>(loader, new GameParser());
            var game = gameExecutor.Run(gameUrl);

            Console.WriteLine("Done...");
            Console.ReadKey();
        }
    }
}
