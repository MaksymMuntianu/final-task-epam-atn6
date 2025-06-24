using Core.WebDriver;
using OpenQA.Selenium;

namespace PageObjects;

public class InventoryPage(WebDriverWrapper driver)
{
    private readonly By _appLogo = By.XPath("//div[@class='app_logo']");
    private readonly WebDriverWrapper _driver = driver ?? throw new ArgumentNullException(nameof(driver));

    public string GetLogoText() => _driver.FindElement(_appLogo).Text;
}