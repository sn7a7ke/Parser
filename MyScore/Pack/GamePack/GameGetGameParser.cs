using MyScore.Models.Football;
using Parser;

namespace MyScore.Pack.GamePack
{
    public class GameGetGameParser : Parser<Game>
    {
        public override Game Parse()
        {
            var sum = new GameSummary();

            sum.Country = InnerTextSplit("//*[@id=\"detcon\"]/div[2]/div[1]/span[2]", 0, '"', ':');

            sum.League = InnerText("//*[@id=\"detcon\"]/div[2]/div[1]/span[2]/a", t => 
            {
                var p = t.Split('-');
                var s = p[p.Length - 1].Trim();
                return t.Substring(0, t.Length - s.Length).Trim(' ', '-');
            });

            sum.Round = InnerTextSplit("//*[@id=\"detcon\"]/div[2]/div[1]/span[2]/a", -1, '-');

            sum.DateTime = InnerText("//*[@id=\"utime\"]");

            sum.HomeTeam = InnerText("//*[@id=\"flashscore\"]/div[1]/div[1]/div[2]/div/div/a");
            
            sum.AwayTeam = InnerText("//*[@id=\"flashscore\"]/div[1]/div[3]/div[2]/div/div/a");

            sum.ScoreHomeTeam = InnerText("//*[@id=\"event_detail_current_result\"]/span[1]");

            sum.ScoreAwayTeam = InnerText("//*[@id=\"event_detail_current_result\"]/span[2]/span[2]");

            sum.ScoreFTHomeTeam = InnerText("//*[@id=\"event_detail_current_result\"]/span[3]/span[1]");

            sum.ScoreFTAwayTeam = InnerText("//*[@id=\"event_detail_current_result\"]/span[3]/span[2]/span[2]");

            sum.Completed = InnerText("//*[@id=\"flashscore\"]/div[1]/div[2]/div[2]");

            sum.ScoreHalfHomeTeam = InnerText("//*[@id=\"summary-content\"]/div[1]/div[1]/div[2]/span[1]"); //wait loading

            sum.ScoreHalfAwayTeam = InnerText("//*[@id=\"summary-content\"]/div[1]/div[1]/div[2]/span[2]");

            sum.Judge = InnerTextSplit("//*[@id=\"summary-content\"]/div[2]/div/div/div[2]/div[1]", 1, ':', ',');

            sum.Attendance = InnerTextSplit("//*[@id=\"summary-content\"]/div[2]/div/div/div[2]/div[2]", 1, ':', ',');
            
            sum.Stadium = InnerTextSplit("//*[@id=\"summary-content\"]/div[2]/div/div/div[2]/div[3]", 1, ':', ',');

            var game = new Game();
            game.Summary = sum;
            return game;
        }
    }
}
