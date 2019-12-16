using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.Threading;

namespace SeleniumProvider
{
    public class WebDriverProvider
    {
        public IWebDriver Driver { get; private set; }

        public string Source => Driver.PageSource;

        public WebDriverProvider()
        {
            Driver = GetWebDriver();
        }

        private IWebDriver GetWebDriver()
        {
            var options = new ChromeOptions();
            options.AddUserProfilePreference("intl.accept_languages", "en");
            options.AddUserProfilePreference("disable-popup-blocking", "true");
            options.AddArguments("--allow-no-sandbox-job");
            options.AddArguments("--ignore-certificate-errors");
            options.AddArguments("--no-sandbox");
            options.AddArguments("--window-size=1920,1080");
            //options.AddArgument("--headless");
            options.AddAdditionalCapability("useAutomationExtension", false);
            //options.AddUserProfilePreference("download.default_directory", downloadDirectory);
            options.AddUserProfilePreference("download.prompt_for_download", false);
            options.AddUserProfilePreference("disable-popup-blocking", "true");

            var dir = AppDomain.CurrentDomain.BaseDirectory;
            return new ChromeDriver(dir, options, TimeSpan.FromSeconds(120));
        }

        public void GoTo(string url, string xPath = null)
        {
            Driver.Url = url;
            if (!string.IsNullOrEmpty(xPath))
                WaitElement(xPath);
        }

        public void SendKeys(string xPath, string text)
        {
            Driver.FindElement(By.XPath(xPath)).SendKeys(text);
        }

        public void Click(string xPath)
        {
            Driver.FindElement(By.XPath(xPath)).Click();
        }

        public ReadOnlyCollection<IWebElement> FindElements(string xPath)
        {
            return Driver.FindElements(By.XPath(xPath));
        }

        public bool WaitElement(string xPath, int seconds = 60)
        {
            var wait = new WebDriverWait(Driver, new TimeSpan(0, 0, seconds));
            try
            {
                wait.Until(condition =>
                {
                    try
                    {
                        return Driver.FindElement(By.XPath(xPath)).Displayed;
                    }
                    catch (StaleElementReferenceException)
                    {
                        return false;
                    }
                    catch (NoSuchElementException)
                    {
                        return false;
                    }
                });
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        /// <summary>
        /// Wait for element to load
        /// </summary>
        /// <param name="xPath">xPath to element</param>
        /// <param name="keyToSend">key to send</param>
        /// <param name="seconds">appearance time</param>
        /// <param name="ms">interval between sending keystrokes</param>
        /// <param name="repeat">number of keystrokes</param>
        /// <returns></returns>
        public bool WaitElement(string xPath, string keyToSend, int seconds = 60, int ms = 50, int repeat = 10)
        {
            if (WaitElement(xPath, seconds))
            {
                Click(xPath);
                Actions actions = new Actions(Driver);
                for (int i = 0; i < repeat; i++)
                {
                    actions.SendKeys(keyToSend).Build().Perform();
                    Thread.Sleep(ms);
                }
                return true;
            }
            return false;
        }

        public void CloseBrowser() => Driver.Close();

        public void Quit() => Driver.Quit();
    }
}
