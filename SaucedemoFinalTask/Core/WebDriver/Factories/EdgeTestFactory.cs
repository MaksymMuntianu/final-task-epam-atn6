using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace Core.WebDriver.Factories;

internal class EdgeTestFactory : ITestFactory
{
    public IWebDriver CreateWebDriver()
    {
        var service = EdgeDriverService.CreateDefaultService();
        var options = new EdgeOptions();
        options.AddArgument("disable-infobars");
        options.AddArgument("inprivate");

        return new EdgeDriver(service, options, TimeSpan.FromSeconds(30));
    }
}