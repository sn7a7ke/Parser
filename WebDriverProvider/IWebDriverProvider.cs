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
        ReadOnlyCollection<IWebElement> FindElements(string xPath);
        void GoTo(string url, string xPath = null);
        void Quit();
        void SendKeys(string xPath, string text);
        bool WaitElement(string xPath, int seconds = 60);
        bool WaitElement(string xPath, Action<IWebDriver> action, int seconds = 60);
    }
}