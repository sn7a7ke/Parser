using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.Threading;

namespace SeleniumProvider
{
    public class WebDriverProvider : IWebDriverProvider
    {
        public IWebDriver Driver { get; private set; }

        public string Source => Driver.PageSource;

        public WebDriverProvider() => Driver = GetWebDriver();

        public WebDriverProvider(IWebDriver driver) => Driver = driver;

        private IWebDriver GetWebDriver()
        {
            var options = new ChromeOptions();
            options.AddUserProfilePreference("intl.accept_languages", "en");
            options.AddUserProfilePreference("disable-popup-blocking", "true");
            options.AddUserProfilePreference("profile.default_content_setting_values.images", 2);
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
                WaitEnabledElement(xPath);
        }

        public void SendKeys(string xPath, string text)
        {
            Driver.FindElement(By.XPath(xPath)).SendKeys(text);
        }

        public void Click(string xPath)
        {
            ScrollTo(xPath);
            Driver.FindElement(By.XPath(xPath)).Click();
        }

        public void ScrollTo(string xPath)
        {
            var element = Driver.FindElement(By.XPath(xPath));
            Actions actions = new Actions(Driver);
            actions.MoveToElement(element);
            actions.Perform();
        }

        public IWebElement FindElement(string xPath)
        {
            try
            {
                return Driver.FindElement(By.XPath(xPath));
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }

        public ReadOnlyCollection<IWebElement> FindElements(string xPath)
        {
            return Driver.FindElements(By.XPath(xPath));
        }

        public void Wait(int ms = 500) => Thread.Sleep(ms);

        public bool WaitFor(Predicate<IWebDriver> pending, int seconds = 60)
        {
            var wait = new WebDriverWait(Driver, new TimeSpan(0, 0, seconds));
            try
            {
                wait.Until(condition =>
                {
                    try
                    {
                        return pending.Invoke(Driver);
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

        public bool WaitEnabledElement(string xPath, int seconds = 60)
        {
            return WaitFor((d) => d.FindElement(By.XPath(xPath)).Enabled, seconds);
        }

        public bool WaitDisplayedElement(string xPath, int seconds = 60)
        {
            return WaitFor( (d) => d.FindElement(By.XPath(xPath)).Displayed, seconds);
        }

        /// <summary>
        /// Wait for the element to load, and then follow the steps on the page to wait for all content to load
        /// </summary>
        /// <param name="xPath">xPath to element</param>
        /// <param name="action">steps to be taken</param>
        /// <param name="seconds">appearance time</param>
        /// <returns></returns>
        public bool WaitDisplayedElement(string xPath, Predicate<IWebDriver> action, int seconds = 60)
        {
            if (WaitDisplayedElement(xPath, seconds))
                return action.Invoke(Driver);                
            return false;
        }

        /// <summary>
        /// The steps on the page to wait for all content to load
        /// </summary>
        /// <param name="xPath">xPath to element</param>
        /// <param name="keyToSend">key to send</param>
        /// <param name="ms">interval between sending keystrokes</param>
        /// <param name="repeat">number of keystrokes</param>
        /// <returns></returns>
        public bool WaitingAction(IWebDriver driver, string xPath, string keyToSend, int ms = 50, int repeat = 10)
        {
            var isPresent = WaitDisplayedElement(xPath);
            if (isPresent)
            {
                driver.FindElement(By.XPath(xPath)).Click();
                Actions actions = new Actions(driver);
                for (int i = 0; i < repeat; i++)
                {
                    actions.SendKeys(keyToSend).Build().Perform();
                    Wait(ms);
                }
            }
            return isPresent;
        }

        public void HoverElement(string xPath)
        {
            var element = Driver.FindElement(By.XPath(xPath));
            Actions action = new Actions(Driver);
            action.MoveToElement(element).Perform();
        }

        public void CloseBrowser() => Driver.Close();

        public void Quit() => Driver.Quit();
    }
}
