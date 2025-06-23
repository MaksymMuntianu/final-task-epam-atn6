using Core.WebDriver.Factories;
using OpenQA.Selenium;

namespace Core.WebDriver
{
    public class WebDriverWrapper : IDisposable
    {
        private readonly IWebDriver _driver;

        private const int WaitTimeInSeconds = 10;

        private bool _disposed;

        private static readonly ThreadLocal<WebDriverWrapper?> Instance = new();


        private WebDriverWrapper(BrowserType browserType)
        {
            _driver = FactoryProvider.GetFactory(browserType).CreateWebDriver();
        }

        public static WebDriverWrapper GetInstance(BrowserType browserType)
        {
            return Instance.Value ?? (Instance.Value = new WebDriverWrapper(browserType));
        }

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