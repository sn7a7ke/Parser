using SeleniumProvider;

namespace MyScore.Pack.GamePack
{
    public class GamePageAction
    {
        private readonly IWebDriverProvider _provider;

        public GamePageAction(IWebDriverProvider provider)
        {
            _provider = provider;
        }

        public void LiveCentre() => _provider.Click("//a[@id=\"a-match-summary\"]");

        public void Timeline() => _provider.Click("//a[@id=\"a-match-timeline\"]");

        public void Statistics() => _provider.Click("//a[@id=\"a-match-statistics\"]");

        public void Lineups() => _provider.Click("//a[@id=\"a-match-lineups\"]");
    }
}
