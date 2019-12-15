using HtmlAgilityPack;
using SeleniumProvider;

namespace Parser
{
    public class SeleniumLoader<T> : ILoader<T>
    {
        private readonly WebDriverProvider _provider;
        private readonly HtmlWeb _web;
        private readonly IUrl<T> _url;

        public SeleniumLoader(IUrl<T> url)
        {
            _provider = new WebDriverProvider();
            _web = new HtmlWeb();
            _url = url;
        }

        public HtmlDocument GetPage(T details)
        {
            var url = _url.Get(details);
            _provider.GoTo(url, "//*[@class=\"event event--results\"]");
            var html = new HtmlDocument();
            html.LoadHtml(_provider.Source);
            return html;
        }
    }
}
