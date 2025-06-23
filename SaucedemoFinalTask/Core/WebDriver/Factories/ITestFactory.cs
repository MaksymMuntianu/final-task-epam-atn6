using OpenQA.Selenium;

namespace Core.WebDriver.Factories;

internal interface ITestFactory
{
    IWebDriver CreateWebDriver();
}