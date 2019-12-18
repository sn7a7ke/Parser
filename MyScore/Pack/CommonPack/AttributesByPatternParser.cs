using HtmlAgilityPack;
using Parser;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MyScore.Pack.CommonPack
{
    public class AttributesByPatternParser : Parser<List<string>>
    {
        public string XPath { get; set; } = "";

        public string Attribute { get; set; } = "";

        public bool IsMyLeague{ get; set; } = true;

        public string EndOfMyLeaguesClassPattern { get; set; } = "^event__header--no-my-games$";

        public string AttributePattern { get; set; } = ".*";

        public string ClassNamePattern { get; set; } = "";

        public override List<string> Parse()
        {
            var results = new List<string>();
            var parentNode = Document.DocumentNode.SelectSingleNode(XPath);
            var rgx = new Regex(AttributePattern);
            foreach (var node in parentNode.ChildNodes)
            {
                if (IsMyLeague && EndOfSearchingForMyLeagues(node))
                    break;
                var attribute = node.Attributes[Attribute]?.Value;
                if (!string.IsNullOrEmpty(attribute) && rgx.IsMatch(attribute)
                    && (ContainClass(node)))
                    results.Add(attribute);
            }
            return results;            
        }

        private bool EndOfSearchingForMyLeagues(HtmlNode node)
        {
            var endClass = new Regex(EndOfMyLeaguesClassPattern);
            return node.GetClasses().Any(c => endClass.IsMatch(c));
        }

        private bool ContainClass(HtmlNode node)
        {
            var rgxClass = new Regex(ClassNamePattern);
            return string.IsNullOrEmpty(ClassNamePattern) || node.GetClasses().Any(c => rgxClass.IsMatch(c));
        }
    }
}
