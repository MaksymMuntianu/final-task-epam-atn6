using OpenQA.Selenium;

namespace Core.WebDriver.Factories;

/// <summary>Defines a factory interface for creating instances of <see cref="IWebDriver"/>.</summary>
internal interface ITestFactory
{
    IWebDriver CreateWebDriver();
}