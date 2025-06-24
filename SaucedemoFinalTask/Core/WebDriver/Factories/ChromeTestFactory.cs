using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Core.WebDriver.Factories;

/// <summary>Provides a factory for creating instances of <see cref="IWebDriver" /> configured for Chrome.</summary>
internal class ChromeTestFactory : ITestFactory
{
    /// <summary>Creates and initializes a new instance of a Chrome WebDriver.</summary>
    /// <remarks>
    ///     The WebDriver is configured with default settings, including incognito mode and the
    ///     "disable-infobars" argument. The method uses a default ChromeDriverService and applies a 30-second command timeout.
    /// </remarks>
    /// <returns>An <see cref="IWebDriver" /> instance configured for Chrome with the specified options.</returns>
    public IWebDriver CreateWebDriver()
    {
        var service = ChromeDriverService.CreateDefaultService();
        var options = new ChromeOptions();
        options.AddArgument("disable-infobars");
        options.AddArgument("--incognito");

        return new ChromeDriver(service, options, TimeSpan.FromSeconds(30));
    }
}