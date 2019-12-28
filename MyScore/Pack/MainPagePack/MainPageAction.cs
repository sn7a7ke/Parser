using SeleniumProvider;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MyScore.Pack.MainPagePack
{
    public class MainPageAction
    {
        private readonly IWebDriverProvider _provider;

        public MainPageAction(IWebDriverProvider provider)
        {
            _provider = provider;
        }

        public void MoreCountry()
        {
            _provider.Click("//li[contains(@class,\"show-more\")]");
        }

        public void ChooseLeague(string id, string cl)
        {
            _provider.Click($"//li[@id=\"{id}\"]");
            _provider.Wait();
            _provider.Click($"//li[@id=\"{id}\"]/descendant::span[contains(@class,\"{cl}\")]");
        }

        public void AddMyLeagues(List<string> source)
        {
            MoreCountry();
            _provider.Wait();
            foreach (var s in source)
            {
                var num = Regex.Match(s, @"_(\w+)_").Groups[1].Value;
                var id = "lmenu_" + num;
                ChooseLeague(id, s);                
            }
        }
        //public void ChangeMyLeagues
    }
}
