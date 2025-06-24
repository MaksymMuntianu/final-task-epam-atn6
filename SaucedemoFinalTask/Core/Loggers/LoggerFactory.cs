namespace Core.Loggers;

public class LoggerFactory
{
    public static ILoggerAdapter CreateLogger(LoggerType loggerType)
    {
        return loggerType switch
        {
            LoggerType.NLog => new NLogAdapter("NLog"),
            LoggerType.Log4Net => new Log4NetAdapter(),
            _ => throw new ArgumentOutOfRangeException(nameof(loggerType), loggerType, null),
        };
    }
}