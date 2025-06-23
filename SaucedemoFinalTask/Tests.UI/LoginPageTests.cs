using Core;
using Core.WebDriver;
using FluentAssertions;
using PageObjects;

namespace Tests.UI;

[TestClass]
public sealed class LoginPageTests
{
    private static string _appUrl;
    private static BrowserType _browserType;
    private WebDriverWrapper _driver;

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        // Ensure the License of Fluent Assersion is accepted before running tests.
        License.Accepted = true;

        // Initialize configuration settings.
        var config = new Configuration();

        Enum.TryParse(config.BrowserType, true, out _browserType);
        _appUrl = config.AppUrl;
    }

    [TestInitialize]
    public void Setup()
    {
        // Ensure the WebDriver instance is initialized before each test
        _driver = WebDriverWrapper.GetInstance(_browserType);

        _driver.MaximizeAndSetWaits(TimeSpan.FromSeconds(10));
        _driver.NavigateTo(_appUrl);
    }

    [TestMethod]
    public void Login_WithEmptyCredentials_ReturnsErrorMessage()
    {
        // Arrange
        var loginPage = new LoginPage(_driver);

        // Act
        loginPage.EnterUsername(string.Empty);
        loginPage.EnterPassword(string.Empty);
        loginPage.ClickLoginButton();

        // Assert
        var errorMessage = loginPage.GetErrorMessage();
        errorMessage.Should().Contain("Username is required");
    }

    [TestMethod]
    public void Login_WithUsernameAndWithoutPassword_ReturnsErrorMessage()
    {
        // Arrange
        var loginPage = new LoginPage(_driver);

        // Act
        loginPage.EnterUsername("standard_user");
        loginPage.EnterPassword("secret_sauce");
        loginPage.ClearPasswordInput();
        loginPage.ClickLoginButton();

        // Assert
        var errorMessage = loginPage.GetErrorMessage();
        errorMessage.Should().Contain("Password is required");
    }

    // Excluded locked_out_user because it is not a valid test case for successful login,
    // and performance_glitch_user to speed up the test suite.
    [DataTestMethod]
    [DataRow("standard_user", "secret_sauce")]
    [DataRow("problem_user", "secret_sauce")]
    [DataRow("error_user", "secret_sauce")]
    [DataRow("visual_user", "secret_sauce")]
    public void Login_WithCorrectCredentials_InventoryHasCorrectDashboardTitle(string login, string password)
    {
        // Arrange
        var loginPage = new LoginPage(_driver);
        var inventoryPage = new InventoryPage(_driver);

        // Act
        loginPage.EnterUsername(login);
        loginPage.EnterPassword(password);
        loginPage.ClickLoginButton();

        // Assert
        var logoText = inventoryPage.GetLogoText();
        logoText.Should().Be("Swag Labs", "because the user should see the Swag Labs title on the dashboard after successful login");
    }

    [TestCleanup]
    public void Cleanup()
    {
        // Ensure the WebDriver instance is disposed after each test
        _driver?.Dispose();
    }
}