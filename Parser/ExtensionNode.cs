using HtmlAgilityPack;
using System;
using System.Linq;

namespace Parser
{
    public static class ExtensionNode
    {
        public static bool ContainClasses(this HtmlNode node, params string[] classes)
        {
            foreach (var cl in classes)
            {
                if (!node.GetClasses().Any(c => c == cl))
                    return false;
            }
            return true;
        }

        public static bool ContainClass(this HtmlNode node, string className)
        {
            var res = node.GetClasses().Any(c => c.Contains(className));
            return res;
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
    }
}
