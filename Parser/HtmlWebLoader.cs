using HtmlAgilityPack;

namespace Parser
{
    public class HtmlWebLoader<T> : ILoader<T>
    {
        private readonly HtmlWeb _web;
        private readonly IUrl<T> _url;

        public HtmlWebLoader(IUrl<T> url)
        {
            _web = new HtmlWeb();
            _url = url;
        }

        //public async Task<HtmlDocument> GetPageAsync(T details)
        //{
        //    var url = _url.Get(details);
        //    var response = await _web.LoadFromWebAsync(url);
        //    //if (response.ParseErrors != null && response.ParseErrors.Any())
        //    //    return null;
        //    return response;
        //}

        public HtmlDocument GetPage(T details)
        {
            var url = _url.Get(details);
            var response = _web.Load(url);
            return response;
        }
    }
}
