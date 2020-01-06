using HtmlAgilityPack;
using OpenQA.Selenium;
using Parser.Interfaces;
using SeleniumProvider;

namespace Parser
{
    public class SeleniumLoader : WebDriverProvider, ILoader
    {
        private readonly HtmlDocument _html = new HtmlDocument();

        public SeleniumLoader() : base() { }

        public SeleniumLoader(IWebDriver driver) : base(driver) { }

        public HtmlDocument Document
        {
            get
            {
                _html.LoadHtml(Source);
                return _html;
            }
        }

        public void GetPage(IUrl url, string pendingXPath = null)
        {
            GoTo(url.Get(), pendingXPath);
        }
    }
}
