using MyScore.Models.Football;
using Parser;

namespace MyScore.Pack.GamePack
{
    public class GameGetGameParser : Parser<Game>
    {
        public override Game Parse()
        {
            var sum = new GameSummary();

            sum.Country = InnerTextNodeSplit("//*[@id=\"detcon\"]/div[2]/div[1]/span[2]", 0, '"', ':');

            sum.League = InnerTextNode("//*[@id=\"detcon\"]/div[2]/div[1]/span[2]/a", t => 
            {
                var p = t.Split('-');
                var s = p[p.Length - 1].Trim();
                return t.Substring(0, t.Length - s.Length).Trim(' ', '-');
            });

            sum.Round = InnerTextNodeSplit("//*[@id=\"detcon\"]/div[2]/div[1]/span[2]/a", -1, '-');

            sum.DateTime = InnerTextNode("//*[@id=\"utime\"]");

            sum.HomeTeam = InnerTextNode("//*[@id=\"flashscore\"]/div[1]/div[1]/div[2]/div/div/a");
            
            sum.AwayTeam = InnerTextNode("//*[@id=\"flashscore\"]/div[1]/div[3]/div[2]/div/div/a");

            sum.ScoreHomeTeam = InnerTextNode("//*[@id=\"event_detail_current_result\"]/span[1]");

            sum.ScoreAwayTeam = InnerTextNode("//*[@id=\"event_detail_current_result\"]/span[2]/span[2]");

            sum.ScoreFTHomeTeam = InnerTextNode("//*[@id=\"event_detail_current_result\"]/span[3]/span[1]");

            sum.ScoreFTAwayTeam = InnerTextNode("//*[@id=\"event_detail_current_result\"]/span[3]/span[2]/span[2]");

            sum.Completed = InnerTextNode("//*[@id=\"flashscore\"]/div[1]/div[2]/div[2]");

            sum.ScoreHalfHomeTeam = InnerTextNode("//*[@id=\"summary-content\"]/div[1]/div[1]/div[2]/span[1]"); //wait loading

            sum.ScoreHalfAwayTeam = InnerTextNode("//*[@id=\"summary-content\"]/div[1]/div[1]/div[2]/span[2]");

            sum.Judge = InnerTextNodeSplit("//*[@id=\"summary-content\"]/div[2]/div/div/div[2]/div[1]", 1, ':', ',');

            sum.Attendance = InnerTextNodeSplit("//*[@id=\"summary-content\"]/div[2]/div/div/div[2]/div[2]", 1, ':', ',');
            
            sum.Stadium = InnerTextNodeSplit("//*[@id=\"summary-content\"]/div[2]/div/div/div[2]/div[3]", 1, ':', ',');

            var game = new Game();
            game.Summary = sum;
            return game;
        }

        private string InnerTextNodeSplit(string xPath, int choice, params char[] separator)
        {
            var inner = InnerTextNode(xPath);
            var parts = inner?.Split(separator);
            if (parts == null || parts.Length == 0)
                return null;
            if (choice >= 0)
            {
                if (parts.Length > choice)
                    return parts[choice].Trim();
            }
            else
            {
                if (parts.Length + choice >= 0)
                    return parts[parts.Length + choice].Trim();
            }
            return null;
        }
    }
}
