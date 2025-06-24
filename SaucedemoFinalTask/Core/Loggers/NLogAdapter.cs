using NLog;

namespace Core.Loggers;

/// <summary>Provides an adapter for logging messages using the NLog framework.</summary>
/// <remarks>This class implements the <see cref="ILoggerAdapter"/> interface, allowing integration with NLog for
/// logging messages at various levels (Info, Warn, Error, Debug). Each log message is automatically trimmed of leading
/// and trailing whitespace before being logged.</remarks>
/// <param name="loggerName">The name of the logger to be used for logging messages.</param>
public class NLogAdapter(string loggerName) : ILoggerAdapter
{
    private readonly Logger _logger = LogManager.GetLogger(loggerName);

    public void Info(string message) => _logger.Info(message.Trim());
    public void Warn(string message) => _logger.Warn(message.Trim());
    public void Error(string message) => _logger.Error(message.Trim());
    public void Debug(string message) => _logger.Debug(message.Trim());
}