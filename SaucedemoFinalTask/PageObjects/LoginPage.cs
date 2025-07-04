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

    public void ClearPasswordInput()
    {
        var passwordInput = _driver.FindElement(_passwordField);

        passwordInput.SendKeys(Keys.Control + "a");
        passwordInput.SendKeys(Keys.Delete);
    }

    public void ClearUsernameInput()
    {
        var usernameInput = _driver.FindElement(_usernameField);

        usernameInput.SendKeys(Keys.Control + "a");
        usernameInput.SendKeys(Keys.Delete);
    }

    public void ClickLoginButton()
    {
        _driver.FindElement(_loginButton).Click();
    }

    public string GetErrorMessage() => _driver.FindElement(_errorMessage).Text;
}