using HtmlAgilityPack;
using Parser.Interfaces;
using SeleniumProvider;

namespace Parser
{
    public class SeleniumLoader : WebDriverProvider, ILoader
    {
        public HtmlDocument GetPage(IUrl url, string pendingXPath = null)
        {
            GoTo(url.Get(), pendingXPath);
            var html = new HtmlDocument();
            html.LoadHtml(Source);
            return html;
        }
    }
}
