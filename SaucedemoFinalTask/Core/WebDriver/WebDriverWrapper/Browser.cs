using Core.WebDriver.Factories;
using OpenQA.Selenium;

namespace Core.WebDriver.WebDriverWrapper 
{
    public partial class WebDriverWrapper : IDisposable
    {
        private readonly TimeSpan _timeout;

        private readonly IWebDriver _driver;

        private const int WaitTimeInSeconds = 10;

        private bool _disposed;

        private static readonly ThreadLocal<WebDriverWrapper?> Instance = new();


        private WebDriverWrapper(BrowserType browserType)
        {
            _driver = FactoryProvider.GetFactory(browserType).CreateWebDriver();
            _timeout = TimeSpan.FromSeconds(WaitTimeInSeconds);
        }

        public static WebDriverWrapper GetInstance(BrowserType browserType)
        {
            return Instance.Value ?? (Instance.Value = new WebDriverWrapper(browserType));
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
