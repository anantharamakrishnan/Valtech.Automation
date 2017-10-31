using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ValTechAUT.AcceptanceTests.Common
{
    public class Driver : IWebDriver
    {
        #region Constants

        private const int SECONDS_TO_WAIT = 5;

        private const int MAX_RETRIES = 5;

        private const int WAIT_BETWEEN_RETRIES = 1000;

        #endregion

        #region Constructor

        public IWebDriver InnerDriver { get; private set; }

        //private ChromeDriver chromeDriver;


        public Driver(IWebDriver driver)
        {
            this.InnerDriver = driver;
        }
        #endregion

        public IWebElement FindElement(By by)
        {
            return this.FindElement(by, new TimeSpan(0, 0, SECONDS_TO_WAIT));
        }

        private IWebElement FindElement(By by, TimeSpan timeout)
        {
            return this.Execute(
                () =>
                {
                    try
                    {
                        var wait = new WebDriverWait(this.InnerDriver, timeout);

                        var element = wait.Until(d => d.FindElement(by));

                        if (element.Displayed && element.Enabled)
                        {
                            return element;
                        }

                        throw new NoSuchElementException(string.Format("Element {0} found, but was not Displayed or Enabled.", by));
                    }
                    catch (WebDriverTimeoutException ex)
                    {
                        throw new NoSuchElementException(string.Format("Unable to find element {0}", by), ex);
                    }
                });
        }

        public ReadOnlyCollection<IWebElement> FindElements(By @by)
        {
            return this.FindElements(by, new TimeSpan(0, 0, SECONDS_TO_WAIT));
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by, TimeSpan timeout)
        {
            try
            {
                return new WebDriverWait(this.InnerDriver, timeout).Until(d => d.FindElements(by));
            }
            catch (WebDriverTimeoutException ex)
            {
                throw new NoSuchElementException(string.Format("Unable to find elements {0}", by), ex);
            }
        }


        public void Dispose()
        {
            this.InnerDriver.Dispose();
        }

        public void Close()
        {
            this.InnerDriver.Close();
        }

        public void Quit()
        {
            this.InnerDriver.Quit();
        }

        public IOptions Manage()
        {
            return this.InnerDriver.Manage();
        }

        public INavigation Navigate()
        {
            return this.InnerDriver.Navigate();
        }

        public ITargetLocator SwitchTo()
        {
            return this.InnerDriver.SwitchTo();
        }


        #region Properties

        public string Url
        {
            get { return this.InnerDriver.Url; }
            set { this.InnerDriver.Url = value; }
        }

        public string Title
        {
            get { return this.InnerDriver.Title; }
        }

        public string PageSource
        {
            get { return this.InnerDriver.PageSource; }
        }

        public string CurrentWindowHandle
        {
            get { return this.InnerDriver.CurrentWindowHandle; }
        }

        public ReadOnlyCollection<string> WindowHandles
        {
            get { return this.InnerDriver.WindowHandles; }
        }

        #endregion


        #region Assertions

        public IWebElement WaitElementEnabled(By by)
        {
            return FindElement(by, d =>
            {
                IWebElement element = d.FindElement(by);
                return element.Enabled ? element : null;
            });
        }

        public IWebElement FindElement(By by, Func<IWebDriver, IWebElement> action, int timeout = 30)
        {
            try
            {
                var wait = new WebDriverWait(this.InnerDriver, TimeSpan.FromSeconds(timeout));
                return wait.Until(action);
            }
            catch (WebDriverTimeoutException ex)
            {
                throw new NoSuchElementException(string.Format("Unable to find element {0}", by), ex);
            }

        }

        public bool FindElementExists(By by, TimeSpan timeout)
        {

            try
            {
                var wait = new WebDriverWait(this.InnerDriver, timeout);

                var element = wait.Until(d => d.FindElement(by));

                return element != null && element.Displayed && element.Enabled;
            }
            catch (WebDriverTimeoutException ex)
            {
                throw new NoSuchElementException(string.Format("Unable to find element {0}", by), ex);
            }

        }

        public bool ElementExists(string xpath)
        {
            try
            {
                var element = this.FindElement(By.XPath(xpath));

                return element != null;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool IsElementVisible(By by, int timeout = 30)
        {
            return WaitElementVisible(by, timeout) != null;
        }

        public IWebElement WaitElementVisible(By by, int timeout = 30)
        {
            WebDriverWait wait = new WebDriverWait(InnerDriver, TimeSpan.FromSeconds(timeout));
            try
            {
                return wait.Until(ExpectedConditions.ElementIsVisible(by));
            }
            catch (WebDriverTimeoutException)
            {
                return null;
            }
        }

        public bool ElementToBeClickable(By by, int timeout = 30)
        {
            WebDriverWait wait = new WebDriverWait(InnerDriver, TimeSpan.FromSeconds(timeout));

            return wait.Until(ExpectedConditions.ElementToBeClickable(by)) != null;
        }

        public bool IsElementVisible(string xpath)
        {
            return IsElementVisible(By.XPath(xpath));
        }

        public bool IsElementEnabled(string xpath)
        {
            try
            {
                var element = this.FindElement(By.XPath(xpath));

                if (element != null)
                {
                    return element.Enabled;
                }

                return false;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void WaitForPageLoad(string pageBusy, IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(this.InnerDriver, TimeSpan.FromSeconds(2));
            wait.Until<bool>(d =>
            {
                try
                {
                    // If the find succeeds, the element exists, and
                    // we want the element to *not* exist, so we want
                    // to return true when the find throws an exception.
                    IWebElement element = d.FindElement(By.XPath(pageBusy));
                    return false;
                }
                catch (NoSuchElementException)
                {
                    return true;
                }
            });
        }

        public bool ValidateUrl(string keyWord)
        {
            var wait = new WebDriverWait(this, new TimeSpan(0, 0, SECONDS_TO_WAIT));

            try
            {
                return wait.Until(x => x.Url == keyWord);

                //This is just a reference for future WebdriverWait
                //return wait.Until(ExpectedConditions.ElementExists(By.XPath("")));
            }
            catch (WebDriverTimeoutException)
            {
                // if we never found it, assume false
                return false;
            }
        }

        public bool ValidatePage(string content)
        {
            var wait = new WebDriverWait(this, new TimeSpan(0, 0, SECONDS_TO_WAIT));

            try
            {
                return wait.Until(x => x.PageSource.Contains(content));
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
        #endregion

        /// <summary>
        /// Runs an action with retry.
        /// </summary>
        /// <param name="action">The action.</param>
        public void Execute(Action action)
        {
            var retryCount = 0;
            bool failed;

            do
            {
                try
                {
                    failed = false;

                    action();
                }
                catch (Exception ex)
                {
                    retryCount++;
                    failed = true;

                    if (retryCount >= MAX_RETRIES)
                    {
                        throw ex;
                    }

                    Thread.Sleep(WAIT_BETWEEN_RETRIES);
                }
            }
            while (failed);
        }

        /// <summary>
        /// Runs an action with retry.
        /// </summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <param name="function">The function.</param>
        /// <returns>Result of type T</returns>
        public T Execute<T>(Func<T> function)
        {
            var retryCount = 0;

            do
            {
                try
                {
                    return function();
                }
                catch (Exception ex)
                {
                    retryCount++;

                    if (retryCount >= MAX_RETRIES)
                    {
                        throw ex;
                    }

                    Thread.Sleep(WAIT_BETWEEN_RETRIES);
                }
            }
            while (true);
        }
    }
}
