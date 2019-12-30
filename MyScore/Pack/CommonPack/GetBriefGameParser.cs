using MyScore.Models.Football;
using Parser;
using System.Text.RegularExpressions;

namespace MyScore.Pack.CommonPack
{
    public class GetBriefGameParser : Parser<BriefGame>
    {
        public string Id { get; set; }

        public override BriefGame Parse()
        {
            var res = new BriefGame();

            res.Link = Id;

            res.Stage = InnerText($"//*[@id=\"{Id}\"]/div[2]/div");

            res.Time = InnerText($"//*[@id=\"{Id}\"]/div[2]");

            res.HomeTeam = InnerText($"//*[@id=\"{Id}\"]/div[3]");

            res.AwayTeam = InnerText($"//*[@id=\"{Id}\"]/div[5]");

            res.ScoreHomeTeam = InnerText($"//*[@id=\"{Id}\"]/div[4]/span[1]");

            res.ScoreAwayTeam = InnerText($"//*[@id=\"{Id}\"]/div[4]/span[2]");

            var inner = InnerText($"//*[@id=\"{Id}\"]/div[6]");

            if (inner != null)
            {
                var rgx = new Regex(@"\d+");
                var halvesScore = rgx.Match(inner).Groups;
                if (halvesScore.Count == 2)
                {
                    res.ScoreHalfHomeTeam = halvesScore[0].Value;
                    res.ScoreHalfAwayTeam = halvesScore[1].Value; 
                }
            }
            return res;
        }
    }
}
