using Microsoft.Extensions.Configuration;

namespace Core;

/// <summary>
///     Provides access to application configuration settings, such as browser type, application URL, and test data path.
/// </summary>
/// <remarks>
///     This class retrieves configuration values from a configuration source, such as an appsettings.json
///     file or a mock source. Default values are provided for certain settings if they are not specified in the configuration.
/// </remarks>
/// <param name="configuration"></param>
public class Configuration(IConfiguration configuration)
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Configuration" /> class using default settings.
    /// </summary>
    /// <remarks>
    ///     This constructor initializes the configuration by loading settings from the
    ///     "appsettings.json" file located in the current working directory. The file is optional and will be reloaded if
    ///     changes are detected.
    /// </remarks>
    public Configuration() : this(new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", true, true)
        .Build())
    {
    }

    /// <summary>
    ///     Gets the type of browser to be used, as specified in the configuration.
    /// </summary>
    public string BrowserType { get; } = configuration["BrowserType"] ?? "Chrome";

    /// <summary>
    ///     Gets the application URL configured in the application settings.
    /// </summary>
    public string AppUrl { get; } = configuration["AppUrl"] ?? string.Empty;

    /// <summary>
    ///     Gets the file system path to the test data directory.
    /// </summary>
    public string TestDataPath { get; } = configuration["TestDataPath"] ?? string.Empty;
}