using System.Reflection;
using log4net;
using log4net.Config;

namespace Core.Loggers;

/// <summary>Provides an adapter for logging messages using the log4net framework.</summary>
/// <remarks>
///     This class implements the <see cref="ILoggerAdapter" /> interface, allowing integration with log4net
///     for logging messages at various levels (Info, Warn, Error, Debug). The log4net configuration is loaded from a
///     "Log4Net.config" file located in the same directory as the executing assembly. Ensure that the configuration file
///     is present and properly configured before using this adapter.
/// </remarks>
public class Log4NetAdapter : ILoggerAdapter
{
    private readonly ILog _logger;

    /// <summary>Initializes a new instance of the <see cref="Log4NetAdapter" /> class and configures the log4net logging framework.</summary>
    /// <remarks>
    ///     This constructor locates the "Log4Net.config" file in the same directory as the executing
    ///     assembly and configures log4net using the settings defined in that file.
    /// </remarks>
    /// <exception cref="InvalidOperationException">Thrown if the logger cannot be initialized.</exception>
    public Log4NetAdapter()
    {
        var log4NetConfigPath = Path.Combine(
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!,
            "Log4Net.config");

        XmlConfigurator.Configure(new FileInfo(log4NetConfigPath));
        _logger = LogManager.GetLogger(typeof(Log4NetAdapter)) ??
                  throw new InvalidOperationException("Logger not initialized.");
    }

    /// <summary>Logs an informational message to the underlying logging system.</summary>
    public void Info(string message) => _logger.Info(message.Trim());

    /// <summary>Logs a warning message to the underlying logging system.</summary>
    public void Warn(string message) => _logger.Warn(message.Trim());

    /// <summary>Logs an error message to the underlying logging system.</summary>
    public void Error(string message) => _logger.Error(message.Trim());

    /// <summary>Logs a debug message to the underlying logging system.</summary>
    public void Debug(string message) => _logger.Debug(message.Trim());
}