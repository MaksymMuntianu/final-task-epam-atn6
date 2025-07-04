using Core;
using Core.Loggers;
using Core.WebDriver;
using PageObjects;
using Reqnroll.BoDi;

namespace Tests.BDD.Hooks;

[Binding]
public class Hooks
{
    private static string? _appUrl;
    private static BrowserType _browserType;
    private static LoggerType _loggerType;
    private static ILoggerAdapter? _logger;
    private readonly IObjectContainer _objectContainer;
    private readonly ScenarioContext _scenarioContext;
    private WebDriverWrapper? _driver;
    private string _testName;

    public Hooks(ScenarioContext scenarioContext, IObjectContainer objectContainer)
    {
        _scenarioContext = scenarioContext;
        _objectContainer = objectContainer;
        _testName = "UnknownTest";
    }

    [BeforeTestRun]
    public static void BeforeTestRun()
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
            _logger!.Error($"Initialization of {nameof(Hooks)} failed: {ex}");
            throw;
        }
    }

    [BeforeScenario]
    public void Initialize()
    {
        _scenarioContext["TestName"] = _scenarioContext.ScenarioInfo.Title;
        _testName = _scenarioContext.TryGetValue("TestName", out string? name) && name != null
            ? name
            : "UnknownTest";

        try
        {
            _logger!.Debug($"Starting BDD test: {_testName}");
            _logger.Info($"Setup started: {_testName}");
            // Ensure the WebDriver instance is initialized before each test
            _driver = WebDriverWrapper.GetInstance(_browserType);

            _driver.MaximizeAndSetWaits(TimeSpan.FromSeconds(10));
            _driver.NavigateTo(_appUrl!);

            _logger!.Debug($"Initializing LoginPage: {_testName}");
            var loginPage = new LoginPage(_driver);


            _objectContainer.RegisterInstanceAs(_driver);
            _objectContainer.RegisterInstanceAs(loginPage);
            _objectContainer.RegisterInstanceAs(_logger);

            _logger.Info($"Setup finished successfully: {_testName} ");
        }
        catch (Exception ex)
        {
            _logger!.Error($"Setup failed: {_testName}; Exception: {ex}");
            throw;
        }
    }

    [AfterScenario]
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