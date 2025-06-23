using Core.WebDriver;
using OpenQA.Selenium;

namespace PageObjects;

public class LoginPage(WebDriverWrapper driver)
{
    private readonly WebDriverWrapper _driver = driver ?? throw new ArgumentNullException(nameof(driver));
    private readonly By _errorMessage = By.XPath("//*[@data-test='error']");
    private readonly By _loginButton = By.XPath("//*[@id='login-button']");
    private readonly By _passwordField = By.XPath("//*[@id='password']");
    private readonly By _usernameField = By.XPath("//*[@id='user-name']");

    public void EnterUsername(string username)
    {
        _driver.FindElement(_usernameField).SendKeys(username);
    }

    public void EnterPassword(string password)
    {
        _driver.FindElement(_passwordField).SendKeys(password);
    }

    public void ClickLoginButton()
    {
        _driver.FindElement(_loginButton).Click();
    }

    public string GetErrorMessage()
    {
        return _driver.FindElement(_errorMessage).Text;
    }
}