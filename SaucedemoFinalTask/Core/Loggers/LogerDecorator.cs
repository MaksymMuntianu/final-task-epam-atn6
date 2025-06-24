namespace Core.Loggers;

public abstract class LoggerDecorator(ILoggerAdapter innerLogger) : ILoggerAdapter
{
    protected readonly ILoggerAdapter InnerLogger = innerLogger;

    public virtual void Info(string message) => InnerLogger.Info(message);
    public virtual void Warn(string message) => InnerLogger.Warn(message);
    public virtual void Error(string message) => InnerLogger.Error(message);
    public virtual void Debug(string message) => InnerLogger.Debug(message);
}