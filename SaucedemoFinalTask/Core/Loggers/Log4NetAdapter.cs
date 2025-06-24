using log4net;
using System.Reflection;
namespace Core.Loggers;

public class Log4NetAdapter : ILoggerAdapter
{
    private readonly ILog _logger;

    public void Info(string message) => _logger.Info(message.Trim());
    public void Warn(string message) => _logger.Warn(message.Trim());
    public void Error(string message) => _logger.Error(message.Trim());
    public void Debug(string message) => _logger.Debug(message.Trim());

    public Log4NetAdapter()
    {
        var log4NetConfigPath = Path.Combine(
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!,
            "Log4Net.config");

        log4net.Config.XmlConfigurator.Configure(new FileInfo(log4NetConfigPath));
        _logger = LogManager.GetLogger(typeof(Log4NetAdapter)) ?? 
                  throw new InvalidOperationException("Logger not initialized.");
    }
}