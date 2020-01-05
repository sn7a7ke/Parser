using HtmlAgilityPack;
using MyScore.Models.Football;
using Parser;

namespace MyScore.Pack.LeaguePack
{
    public partial class LeagueParser : Parser<League>
    {
        private readonly TeamResultsParser _trParser;

        public override HtmlDocument Document
        {
            get => base.Document;
            set
            {
                base.Document = value;
                _trParser.Document = value;
            }
        }

        public LeagueParser(string xPath = null)
        {
            XPath = (xPath ?? "") + "//div[@id=\"mc\"]";
            _trParser = new TeamResultsParser();
            Pending.AddRange(_trParser.Pending);
        }

        public override League Parse()
        {
            var league = LeagueSummaryParse();

            var teams = _trParser.Parse();

            foreach (var t in teams)
            {
                t.LeagueCode = league.Code;
                foreach (var f in t.Forms)
                {
                    f.TeamCode = t.Code;
                    f.LeagueCode = t.LeagueCode;
                }
            }
            league.Teams = teams;

            return league;
        }

        private League LeagueSummaryParse()
        {
            var league = new League();

            league.Name = Node.DescendantInnerText(".//div[@id=\"fscon\"]//div[contains(@class,\"teamHeader__name\")]");

            league.Code = Node.SelectSingleNode(".//div[@id=\"tomyleagues\"]//span[contains(@class,\"toggleMyLeague\")]")?.AttributeExactlyPattern("class", AttributePatternConstants.LeagueCode);

            league.Country = Node.DescendantInnerText(".//h2[contains(@class,\"tournament\")]//span[contains(@class,\"flag\")]/following-sibling::a");

            league.CountryCode = Node.SelectSingleNode(".//h2[contains(@class,\"tournament\")]//span[contains(@class,\"flag\")]")?.AttributeExactlyPattern("class", AttributePatternConstants.CountryCode);

            return league;
        }
    }
}
