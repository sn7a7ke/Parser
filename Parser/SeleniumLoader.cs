using HtmlAgilityPack;
using OpenQA.Selenium;
using Parser.Interfaces;
using SeleniumProvider;

namespace Parser
{
    public class SeleniumLoader : WebDriverProvider, ILoader
    {
        public SeleniumLoader() : base() { }

        public SeleniumLoader(IWebDriver driver) : base(driver) { }

        public HtmlDocument Document
        {
            get
            {
                var html = new HtmlDocument();
                html.LoadHtml(Source);
                return html;
            }
        }

        public HtmlDocument GetPage(IUrl url, string pendingXPath = null)
        {
            GoTo(url.Get(), pendingXPath);
            return Document;
        }
    }
}
