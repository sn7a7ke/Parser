using HtmlAgilityPack;
using MyScore.Models.Football;
using Parser;
using System.Linq;

namespace MyScore.Pack.GamePack
{
    public class GameIncidentsParser : ListParser<GameIncident>
    {
        public GameIncidentsParser(string xPath = null)
        {
            DescendantPrefix = "./child::div[contains(@class,\"detailMS__incidentRow\")]";
            XPath = (xPath ?? "") + "//div[@id=\"summary-content\"]/div[@class=\"detailMS\"]";
            AddPending();
        }

        public override GameIncident GetDesired(HtmlNode node)
        {
            var incident = new GameIncident();

            incident.Time = node.DescendantInnerText(".//div[contains(@class,\"time-box\")]");

            var mainDiv = node.SelectSingleNode("./div[contains(@class,\"icon-box\")]");

            incident.Type = mainDiv?.GetClasses()?.FirstOrDefault(c => !c.Contains("icon-box"));

            incident.Description = mainDiv?.GetAttributeValue("title", null);

            incident.SubstitutionIn = node.DescendantInnerText(".//span[@class=\"substitution-in-name\"]/a");

            incident.SubstitutionOut = node.DescendantInnerText(".//span[@class=\"substitution-out-name\"]/a");

            incident.Participant = node.DescendantInnerText(".//span[@class=\"participant-name\"]/a");

            incident.Assist = node.DescendantInnerText(".//span[@class=\"assist note-name\"]/a");

            incident.SubIncident = node.DescendantInnerText(".//span[@class=\" subincident-name\"]");

            incident.Note = node.DescendantInnerText(".//span[@class=\" note-name\"]");

            return incident;
        }

    }
}
