using Microsoft.Extensions.Configuration;

namespace Core;

/// <summary>
///     Represents the configuration settings for the application, including browser type, logger type, and application URL.
/// </summary>
/// <param name="configuration">The configuration object used to initialize the settings for mock tests.</param>
public class Configuration(IConfiguration configuration)
{
    public Configuration() : this(new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", true, true)
        .Build())
    {
    }

    /// <summary>Gets the type of browser to be used for testing. Defaults to "Chrome" if not specified in the configuration.</summary>
    public string BrowserType { get; } = configuration["BrowserType"] ?? "Chrome";

    /// <summary>Gets the type of logger to be used for logging. Defaults to "NLog" if not specified in the configuration.</summary>
    public string LoggerType { get; } = configuration["LoggerType"] ?? "NLog";

    /// <summary>
    ///     Gets the URL of the application to be tested. Defaults to "https://www.saucedemo.com/" if not specified in the
    ///     configuration.
    /// </summary>
    public string AppUrl { get; } = configuration["AppUrl"] ?? "https://www.saucedemo.com/";
}