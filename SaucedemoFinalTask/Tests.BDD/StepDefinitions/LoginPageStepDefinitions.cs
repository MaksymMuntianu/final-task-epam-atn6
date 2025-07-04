using Core.Loggers;
using Core.WebDriver;
using FluentAssertions;
using PageObjects;

namespace Tests.BDD.StepDefinitions;

[Binding]
public class LoginPageStepDefinitions
{
    private readonly WebDriverWrapper _driver;
    private readonly ILoggerAdapter _logger;
    private readonly LoginPage _loginPage;
    private readonly string _testName;

    public LoginPageStepDefinitions(ScenarioContext scenarioContext, WebDriverWrapper driver, ILoggerAdapter logger,
        LoginPage loginPage)
    {
        _driver = driver;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Logger cannot be null");
        _loginPage = loginPage;
        _testName = scenarioContext.TryGetValue("TestName", out string? name) && name != null ? name : "UnknownTest";
    }

    [Given("I am on the login page")]
    public void GivenIAmOnTheLoginPage()
    {
        // Navigation to the login page is handled in the test hooks.
    }

    [When("I clear the username and password fields")]
    public void WhenIClearTheUsernameAndPasswordFields()
    {
        _logger.Debug($"Clearing username and password fields: {_testName}");
        _loginPage.ClearUsernameInput();
        _loginPage.ClearPasswordInput();
    }

    [When("I click the login button")]
    public void WhenIClickLoginTheButton()
    {
        _logger.Debug($"Clicking the login button for test: {_testName}");
        _loginPage.ClickLoginButton();
    }

    [Then("I should see an error message that contains {string}")]
    public void ThenIShouldSeeAnErrorMessageThatContains(string message)
    {
        _logger.Debug($"Validating error message: {_testName}");
        var errorMessage = _loginPage.GetErrorMessage();
        errorMessage.Should().Contain(message);
    }

    [When("I clear the password field")]
    public void WhenIClearThePasswordField()
    {
        _logger.Debug($"Clearing the password field for test: {_testName}");
        _loginPage.ClearPasswordInput();
    }

    [When("I enter valid (.*) in the username, and valid (.*) in password fields")]
    [When("I enter {string} in the username, and {string} in the password fields")]
    public void WhenIEnterUsernameAndPassword(string username, string password)
    {
        _logger.Debug($"Entering username: {username} and password: {password} for test: {_testName}");
        _loginPage.EnterUsername(username);
        _loginPage.EnterPassword(password);
    }

    [Then("I should see the inventory page with the title {string} in the dashboard")]
    public void ThenIShouldSeeTheInventoryPageWithTheTitleInTheDashboard(string title)
    {
        var inventoryPage = new InventoryPage(_driver);

        _logger.Debug($"Validating the dashboard's title text: {_testName}");
        var logoText = inventoryPage.GetLogoText();
        logoText.Should().Be("Swag Labs",
            "because the user should see the Swag Labs title on the dashboard after successful login");
    }
}