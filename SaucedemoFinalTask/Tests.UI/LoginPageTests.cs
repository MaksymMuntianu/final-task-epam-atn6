using Core;
using Core.Loggers;
using Core.WebDriver;
using FluentAssertions;
using PageObjects;

namespace Tests.UI;

[TestClass]
public sealed class LoginPageTests
{
    private static string? _appUrl;
    private static BrowserType _browserType;
    private static LoggerType _loggerType;
    private static ILoggerAdapter? _logger;
    private WebDriverWrapper? _driver;
    private string? _testName;
    public TestContext? TestContext { get; set; }


    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        try
        {
            var config = new Configuration();

            if (!Enum.TryParse(config.LoggerType, true, out _loggerType))
                throw new InvalidOperationException($"Invalid LoggerType: {config.LoggerType}");

            _logger = new LoggerBuilder(_loggerType)
                .WithPrefix("Login page test:")
                .WithRepeatGuard()
                .Build();

            _logger.Info("Getting BrowserType and AppUrl from the configuration");
            if (!Enum.TryParse(config.BrowserType, true, out _browserType))
                throw new InvalidOperationException($"Invalid BrowserType: {config.BrowserType}");

            _appUrl = config.AppUrl;

            if (_logger == null) throw new InvalidOperationException("Logger not initialized.");

            if (_appUrl == null) throw new InvalidOperationException("App URL not set.");
        }
        catch (Exception ex)
        {
            _logger!.Error($"Initialization of {nameof(LoginPageTests)} failed: {ex}");
            throw;
        }
    }

    [TestInitialize]
    public void Setup()
    {
        _testName = TestContext?.TestName ?? "UnknownTest";

        try
        {
            _logger!.Debug($"Starting test: {_testName}");
            _logger.Info($"Setup started: {_testName}");
            // Ensure the WebDriver instance is initialized before each test
            _driver = WebDriverWrapper.GetInstance(_browserType);

            _driver.MaximizeAndSetWaits(TimeSpan.FromSeconds(10));
            _driver.NavigateTo(_appUrl!);
            _logger.Info($"Setup finished successfully: {_testName} ");
        }
        catch (Exception ex)
        {
            _logger!.Error($"Setup failed: {_testName}; Exception: {ex}");
            throw;
        }
    }

    [TestMethod]
    public void Login_WithEmptyCredentials_ReturnsErrorMessage()
    {
        try
        {
            // Arrange
            _logger!.Debug($"Initializing LoginPage: {_testName}");
            var loginPage = new LoginPage(_driver!);

            // Act
            _logger.Debug($"Submitting empty credentials {_testName}");
            loginPage.EnterUsername("standard_user");
            loginPage.EnterPassword("secret_sauce");
            loginPage.ClearUsernameInput();
            loginPage.ClearPasswordInput();
            loginPage.ClickLoginButton();

            // Assert
            _logger.Debug($"Validating error message: {_testName}");
            var errorMessage = loginPage.GetErrorMessage();
            errorMessage.Should().Contain("Username is required");
        }
        catch (Exception ex)
        {
            _logger!.Error($"Test failed: {ex}");
            throw;
        }
    }

    [TestMethod]
    public void Login_WithUsernameAndWithoutPassword_ReturnsErrorMessage()
    {
        try
        {
            // Arrange
            _logger!.Debug($"Initializing LoginPage: {_testName}");
            var loginPage = new LoginPage(_driver!);

            // Act
            _logger.Debug($"Submitting a login and a password, then delete the password: {_testName}");
            loginPage.EnterUsername("standard_user");
            loginPage.EnterPassword("secret_sauce");
            loginPage.ClearPasswordInput();
            loginPage.ClickLoginButton();

            // Assert
            _logger.Debug($"Validating error message: {_testName}");
            var errorMessage = loginPage.GetErrorMessage();
            errorMessage.Should().Contain("Password is required");
        }
        catch (Exception ex)
        {
            _logger!.Error($"Test failed: {_testName}; Exception: {ex}");
            throw;
        }
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
        try
        {
            // Arrange
            _logger!.Debug($"Initializing LoginPage: {_testName}");
            var loginPage = new LoginPage(_driver!);

            _logger.Debug($"Initializing InventoryPage: {_testName}");
            var inventoryPage = new InventoryPage(_driver!);

            // Act
            _logger.Debug($"Submitting valid credentials {_testName}");
            loginPage.EnterUsername(login);
            loginPage.EnterPassword(password);
            loginPage.ClickLoginButton();

            // Assert
            _logger.Debug($"Validating the dashboard's title text: {_testName}");
            var logoText = inventoryPage.GetLogoText();
            logoText.Should().Be("Swag Labs",
                "because the user should see the Swag Labs title on the dashboard after successful login");
        }
        catch (Exception ex)
        {
            _logger!.Error($"Test failed: {ex}");
            throw;
        }
    }

    [TestCleanup]
    public void Cleanup()
    {
        try
        {
            // Ensure the WebDriver instance is disposed after each test
            _logger!.Debug($"Test passed: {_testName}");
            _driver?.Dispose();
            _logger.Info($"Cleanup completed: {_testName}");
        }
        catch (Exception ex)
        {
            _logger!.Error($"Cleanup failed: {_testName}; Exception: {ex}");

            throw;
        }
    }
}