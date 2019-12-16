using HtmlAgilityPack;
using Parser.Interfaces;
using Parser.MyScore.Models.Football;
using System;

namespace Parser.MyScore
{
    public class GameDetailsParser : IParser<Game>
    {
        public Game Parse(HtmlDocument document)
        {
            // filter html elements on the basis of class name
            //IEnumerable nodes = doc.DocumentNode.Descendants().Where(n => n.HasClass("mw-jump-link"));

            var sum = new GameSummary();
            string temp;
            string[] parts;
            HtmlNode node;

            node = document.DocumentNode.SelectSingleNode("//*[@id=\"detcon\"]/div[2]/div[1]/span[2]");
            temp = node?.InnerText;
            parts = temp?.Split('"', ':');
            if (parts?.Length > 0)
                sum.Country = parts[0].Trim();

            node = document.DocumentNode.SelectSingleNode("//*[@id=\"detcon\"]/div[2]/div[1]/span[2]/a");
            temp = node?.InnerText;
            parts = temp?.Split('-');
            if (parts?.Length < 2)
                throw new ArgumentOutOfRangeException(nameof(parts));
            sum.Round = parts[parts.Length - 1].Trim();
            sum.League = temp.Substring(0, temp.Length - sum.Round.Length).Trim(' ', '-');

            node = document.DocumentNode.SelectSingleNode("//*[@id=\"utime\"]");
            sum.DateTime = node?.InnerText;

            node = document.DocumentNode.SelectSingleNode("//*[@id=\"flashscore\"]/div[1]/div[1]/div[2]/div/div/a");
            sum.HomeTeam = node?.InnerText;

            node = document.DocumentNode.SelectSingleNode("//*[@id=\"flashscore\"]/div[1]/div[3]/div[2]/div/div/a");
            sum.AwayTeam = node?.InnerText;

            node = document.DocumentNode.SelectSingleNode("//*[@id=\"event_detail_current_result\"]/span[1]");
            sum.ScoreHomeTeam = node?.InnerText;

            node = document.DocumentNode.SelectSingleNode("//*[@id=\"event_detail_current_result\"]/span[2]/span[2]");
            sum.ScoreAwayTeam = node?.InnerText;

            node = document.DocumentNode.SelectSingleNode("//*[@id=\"event_detail_current_result\"]/span[3]/span[1]");
            sum.ScoreFTHomeTeam = node?.InnerText;

            node = document.DocumentNode.SelectSingleNode("//*[@id=\"event_detail_current_result\"]/span[3]/span[2]/span[2]");
            sum.ScoreFTAwayTeam = node?.InnerText;

            node = document.DocumentNode.SelectSingleNode("//*[@id=\"flashscore\"]/div[1]/div[2]/div[2]");
            sum.Completed = node?.InnerText;

            node = document.DocumentNode.SelectSingleNode("//*[@id=\"summary-content\"]/div[1]/div[1]/div[2]/span[1]");
            sum.ScoreHalfHomeTeam = node?.InnerText.Trim();

            node = document.DocumentNode.SelectSingleNode("//*[@id=\"summary-content\"]/div[1]/div[1]/div[2]/span[2]");
            sum.ScoreHalfAwayTeam = node?.InnerText.Trim();

            node = document.DocumentNode.SelectSingleNode("//*[@id=\"summary-content\"]/div[2]/div/div/div[2]/div[1]");
            temp = node?.InnerText;
            parts = temp?.Split(':', ',');
            if (parts?.Length > 1)
                sum.Judge = parts[1].Trim();

            node = document.DocumentNode.SelectSingleNode("//*[@id=\"summary-content\"]/div[2]/div/div/div[2]/div[2]");
            temp = node?.InnerText;
            parts = temp?.Split(':', ',');
            if (parts?.Length > 1)
                sum.Attendance = parts[1].Trim();

            node = document.DocumentNode.SelectSingleNode("//*[@id=\"summary-content\"]/div[2]/div/div/div[2]/div[3]");
            temp = node?.InnerText;
            parts = temp?.Split(':', ',');
            if (parts?.Length > 1)
                sum.Stadium = parts[1].Trim();

            var game = new Game();
            game.Summary = sum;
            return game;
        }
    }
}
