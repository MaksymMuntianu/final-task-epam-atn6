namespace Core.Loggers;

public class PrefixDecorator(ILoggerAdapter innerLogger, string prefix) : LoggerDecorator(innerLogger)
{
    public override void Info(string message) => InnerLogger.Info($"{prefix} {message}".Trim());
    public override void Warn(string message) => InnerLogger.Warn($"{prefix} {message}".Trim());
    public override void Error(string message) => InnerLogger.Error($"{prefix} {message}".Trim());
    public override void Debug(string message) => InnerLogger.Debug($"{prefix} {message}".Trim());
}