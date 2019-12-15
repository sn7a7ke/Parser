using HtmlAgilityPack;
using Parser.MyScore.Models.Football;
using System.Text.RegularExpressions;

namespace Parser.MyScore
{
    public class GameDetailsParser : IParser<Game>
    {
        public Game Parse(HtmlDocument document)
        {
            ////var
            //var Game = new Game();
            //var parentNode = document.DocumentNode.SelectSingleNode("//*[@id=\"live-table\"]/div[1]/div/div");
            //var rgx = new Regex(@"^g_1_\w+");
            //foreach (var node in parentNode.ChildNodes)
            //{
            //    var id = node.Attributes["id"]?.Value;
            //    if (!string.IsNullOrEmpty(id) && rgx.IsMatch(id))
            //        results.Add(id.Substring(4));
            //}

            throw new System.NotImplementedException();

            //return results;
        }
    }
}
