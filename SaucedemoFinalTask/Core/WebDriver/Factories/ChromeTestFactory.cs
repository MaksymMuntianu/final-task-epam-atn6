using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Core.WebDriver.Factories;

internal class ChromeTestFactory : ITestFactory
{
    public IWebDriver CreateWebDriver()
    {
        var service = ChromeDriverService.CreateDefaultService();
        var options = new ChromeOptions();
        options.AddArgument("disable-infobars");
        options.AddArgument("--incognito");

        return new ChromeDriver(service, options, TimeSpan.FromSeconds(30));
    }
}