using NLog;

namespace Core.Loggers;

public class NLogAdapter(string loggerName) : ILoggerAdapter
{
    private readonly Logger _logger = LogManager.GetLogger(loggerName);

    public void Info(string message) => _logger.Info(message.Trim());
    public void Warn(string message) => _logger.Warn(message.Trim());
    public void Error(string message) => _logger.Error(message.Trim());
    public void Debug(string message) => _logger.Debug(message.Trim());
}