using HtmlAgilityPack;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Parser
{
    public static class ExtensionNode
    {
        public static bool ContainClasses(this HtmlNode node, params string[] classes)
        {
            var nodeClasses = node.GetClasses();
            return !classes.Any(cl => !nodeClasses.Any(c => c == cl));
        }

        public static bool ContainClass(this HtmlNode node, string className)
        {
            return node.GetClasses().Any(c => c.Contains(className));
        }

        public static string InnerTextByClass(this HtmlNode node, string className)
        {
            return node.InnerTextByClass(className, s => s);
        }

        public static string InnerTextByClass(this HtmlNode node, string className, Func<string, string> func)
        {
            return func(node.Descendants().FirstOrDefault(d => d.HasClass(className))?.InnerText)?.Trim();
        }

        public static string InnerTextSplit(this HtmlNode node, int choice, params char[] separator)
        {
            var parts = node?.InnerText?.Split(separator);
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

        public static string Attribute(this HtmlNode node, string attributeName, string attributePattern, string containClass = "")
        {
            var attribute = node.Attributes[attributeName]?.Value;
            if (attribute != null && Regex.IsMatch(attribute, attributePattern) && node.ContainClass(containClass))
                return attribute;
            return null;
        }

        public static string AttributeExactlyPattern(this HtmlNode node, string attributeName, string attributePattern, string containClass = "")
        {
            var res = Regex.Match(node.Attribute(attributeName, attributePattern, containClass) ?? "", attributePattern)?.Value;
            return res == "" ? null : res;
        }
    }
}
