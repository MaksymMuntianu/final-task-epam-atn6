using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace Core.WebDriver.Factories;

/// <summary>Provides a factory for creating instances of <see cref="IWebDriver" /> configured for Microsoft Edge.</summary>
internal class EdgeTestFactory : ITestFactory
{
    /// <summary>Creates and initializes a new instance of an <see cref="IWebDriver" /> configured for Microsoft Edge.</summary>
    /// <remarks>
    ///     The returned <see cref="IWebDriver" /> instance is configured with default settings,
    ///     including disabling informational bars in the browser, and launching the browser in InPrivate mode.
    ///     The method uses a default timeout of 30 seconds for browser initialization.
    /// </remarks>
    /// <returns>An <see cref="IWebDriver" /> instance configured for Microsoft Edge.</returns>
    public IWebDriver CreateWebDriver()
    {
        var service = EdgeDriverService.CreateDefaultService();
        var options = new EdgeOptions();
        options.AddArgument("disable-infobars");
        options.AddArgument("inprivate");

        return new EdgeDriver(service, options, TimeSpan.FromSeconds(30));
    }
}