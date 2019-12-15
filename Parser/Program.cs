using Parser.MyScore;
using System;
using System.Collections.Generic;

namespace Parser
{
    static class Program
    {
        static void Main(string[] args)
        {
            var details = new InitialDetails
            {
                Game = "football",
                Country = "ukraine",
                League = "premier-league-2017-2018",
                Fixture = "results"
            };
            var results = GetLinksOfLeague(details);

            Console.WriteLine("Done...");
            Console.ReadKey();
        }

        private static IEnumerable<string> GetLinksOfLeague(InitialDetails details)
        {
            var parser = new MatchDetailsParser();
            var url = new InitialUrl();
            var loader = new SeleniumLoader<InitialDetails>(url);
            var executor = new Executor<InitialDetails, string>(parser, loader);
            //executor.ReceivedChunk += Executor_ReceivedChunk;
            //executor.Done += Executor_Done;
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
