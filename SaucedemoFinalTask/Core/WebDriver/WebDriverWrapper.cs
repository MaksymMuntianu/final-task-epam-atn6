using Core.WebDriver.Factories;
using OpenQA.Selenium;

namespace Core.WebDriver
{
    /// <summary>Provides a wrapper around the WebDriver to manage browser instances and interactions.</summary>
    public class WebDriverWrapper : IDisposable
    {
        private readonly IWebDriver _driver;

        private bool _disposed;

        /// <summary>Thread-local instance of the WebDriverWrapper to ensure thread safety. </summary>
        private static readonly ThreadLocal<WebDriverWrapper?> Instance = new();


        private WebDriverWrapper(BrowserType browserType)
        {
            _driver = FactoryProvider.GetFactory(browserType).CreateWebDriver();
        }

        /// <summary>Gets an instance of the WebDriverWrapper for the specified browser type.</summary>
        public static WebDriverWrapper GetInstance(BrowserType browserType)
        {
            return Instance.Value ?? (Instance.Value = new WebDriverWrapper(browserType));
        }

        /// <summary>Gets the underlying WebDriver instance.</summary>
        /// <param name="by">Locator to find the element.</param>
        public IWebElement FindElement(By by)
        {
            try
            {
                return _driver.FindElement(by);
            }
            catch (NoSuchElementException)
            {
                throw new NoSuchElementException($"Element not found: {by}");
            }
        }

        /// <summary>Maximizes the browser window and sets the implicit wait time.</summary>
        /// <param name="implicitWaitTime">The time to wait for elements to appear.</param>
        public void MaximizeAndSetWaits(TimeSpan implicitWaitTime)
        {
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = implicitWaitTime;
        }

        /// <summary>Navigates to the specified URL.</summary>
        /// <param name="url">The URL to navigate to.</param>
        public void NavigateTo(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        /// <summary>Disposes the WebDriverWrapper and releases resources.</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _driver.Quit();
                Instance.Value = null;
            }

            _disposed = true;
        }
    }
}