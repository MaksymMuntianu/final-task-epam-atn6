namespace Core.Loggers;

/// <summary>
/// Serves as a base class for implementing decorators that extend or modify the behavior of an <see cref="ILoggerAdapter"/>.
/// </summary>
/// <param name="innerLogger">The inner logger to be decorated.</param>
public abstract class LoggerDecorator(ILoggerAdapter innerLogger) : ILoggerAdapter
{
    protected readonly ILoggerAdapter InnerLogger = innerLogger;

    public virtual void Info(string message) => InnerLogger.Info(message);
    public virtual void Warn(string message) => InnerLogger.Warn(message);
    public virtual void Error(string message) => InnerLogger.Error(message);
    public virtual void Debug(string message) => InnerLogger.Debug(message);
}