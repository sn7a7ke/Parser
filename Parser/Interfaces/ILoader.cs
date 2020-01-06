using HtmlAgilityPack;

namespace Parser.Interfaces
{
    public interface ILoader
    {
        HtmlDocument Document { get; }

        void GetPage(IUrl url, string pendingXPath = null);

        bool WaitEnabledElement(string xPath, int seconds = 60);
    }
}