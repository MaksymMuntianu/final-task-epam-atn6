namespace Core.Loggers;

public class LoggerBuilder(LoggerType loggerType)
{
    private ILoggerAdapter _logger = LoggerFactory.CreateLogger(loggerType);

    public LoggerBuilder WithPrefix(string prefix)
    {
        _logger = new PrefixDecorator(_logger, prefix);
        return this;
    }

    public LoggerBuilder WithRepeatGuard()
    {
        _logger = new RepeatGuardDecorator(_logger);
        return this;
    }

    public ILoggerAdapter Build()
    {
        return _logger;
    }
}