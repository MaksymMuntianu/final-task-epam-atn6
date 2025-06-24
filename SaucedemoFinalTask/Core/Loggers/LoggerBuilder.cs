namespace Core.Loggers;

/// <summary>
/// Provides a builder for creating and configuring an <see cref="ILoggerAdapter"/> instance.
/// </summary>
/// <remarks>The <see cref="LoggerBuilder"/> class allows for the step-by-step configuration of a logger by
/// applying optional decorators, such as a prefix or a repeat guard, before building the final <see cref="ILoggerAdapter"/> instance.</remarks>
/// <param name="loggerType">The type of logger to create.</param>
public class LoggerBuilder(LoggerType loggerType)
{
    private ILoggerAdapter _logger = LoggerFactory.CreateLogger(loggerType);

    /// <summary>Adds a prefix to all log messages produced by the logger.</summary>
    /// <param name="prefix">The string to prepend to each log message.</param>
    /// <returns>The current <see cref="LoggerBuilder"/> instance, allowing for method chaining.</returns>
    public LoggerBuilder WithPrefix(string prefix)
    {
        _logger = new PrefixDecorator(_logger, prefix);
        return this;
    }

    /// <summary>Wraps the current logger with a repeat guard to prevent duplicate log entries.</summary>
    /// <remarks>This method decorates the current logger with a <see cref="RepeatGuardDecorator"/>,  which
    /// ensures that repeated log messages are filtered out. Use this method to  enhance logging behavior by avoiding
    /// redundant log entries.</remarks>
    /// <returns>The current <see cref="LoggerBuilder"/> instance, allowing for method chaining.</returns>
    public LoggerBuilder WithRepeatGuard()
    {
        _logger = new RepeatGuardDecorator(_logger);
        return this;
    }

    /// <summary>Builds and returns the configured <see cref="ILoggerAdapter"/> instance.</summary>
    /// <returns>The <see cref="ILoggerAdapter"/> instance that has been configured.</returns>
    public ILoggerAdapter Build()
    {
        return _logger;
    }
}