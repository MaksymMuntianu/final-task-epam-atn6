namespace Core.Loggers;

/// <summary>A logger decorator that adds a specified prefix to all log messages.</summary>
/// <remarks>
///     This class wraps an existing <see cref="ILoggerAdapter" /> instance and prepends a prefix to each log message
///     before delegating the call to the inner logger. The prefix is trimmed to ensure no leading or trailing whitespace in the final log message.
/// </remarks>
/// <param name="innerLogger">
///     The <see cref="ILoggerAdapter" /> instance to which log messages are forwarded. Cannot be <see langword="null" />.
/// </param>
/// <param name="prefix">
///     The prefix to prepend to each log message. If the prefix is empty or consists only of whitespace, it will be ignored.
/// </param>
public class PrefixDecorator(ILoggerAdapter innerLogger, string prefix) : LoggerDecorator(innerLogger)
{
    public override void Info(string message) => InnerLogger.Info($"{prefix} {message}".Trim());
    public override void Warn(string message) => InnerLogger.Warn($"{prefix} {message}".Trim());
    public override void Error(string message) => InnerLogger.Error($"{prefix} {message}".Trim());
    public override void Debug(string message) => InnerLogger.Debug($"{prefix} {message}".Trim());
}