using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace SeleniumProvider
{
    public interface IWebDriverProvider
    {
        IWebDriver Driver { get; }
        string Source { get; }

        void Click(string xPath);
        void CloseBrowser();
        IWebElement FindElement(string xPath);
        ReadOnlyCollection<IWebElement> FindElements(string xPath);
        void GoTo(string url, string xPath = null);
        void Quit();
        void SendKeys(string xPath, string text);
        void Wait(int ms = 500);
        bool WaitDisplayedElement(string xPath, int seconds = 60);
        bool WaitFor(Predicate<IWebDriver> pending, int seconds = 60);
        bool WaitEnabledElement(string xPath, int seconds = 60);
        void HoverElement(string xPath);
    }
}