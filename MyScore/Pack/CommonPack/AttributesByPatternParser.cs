using Parser;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MyScore.Pack.CommonPack
{
    public class AttributesByPatternParser : Parser<List<string>>
    {
        public string XPath { get; set; } = "";

        public string Attribute { get; set; } = "";

        public bool IsMyLeague { get; set; } = true;

        public string EndOfMyLeaguesClass { get; set; } = "event__header--no-my-games";

        public string AttributePattern { get; set; } = ".*";

        public string ClassContains { get; set; } = "";

        public override List<string> Parse()
        {
            var results = new List<string>();
            var children = Document.DocumentNode.SelectSingleNode(XPath)?.ChildNodes;
            if (children == null)
                return results;
            var rgx = new Regex(AttributePattern);
            foreach (var node in children)
            {
                if (IsMyLeague && node.ContainClass(EndOfMyLeaguesClass))
                    break;
                var attribute = node.Attributes[Attribute]?.Value;
                if (!string.IsNullOrEmpty(attribute) && rgx.IsMatch(attribute)
                    && (ClassContains == "" || node.ContainClass(ClassContains)))
                    results.Add(attribute);
            }
            return results;
        }
    }
}
