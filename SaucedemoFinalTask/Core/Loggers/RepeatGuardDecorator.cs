namespace Core.Loggers;

/// <summary>A logger decorator that prevents consecutive duplicate log messages from being logged.</summary>
/// <remarks>This decorator ensures that if the same log message is logged consecutively, only the first
/// occurrence is processed. Subsequent identical messages are ignored until a different message is logged. This
/// behavior applies to all log levels (Info, Warn, Error, Debug).</remarks>
/// <param name="innerLogger">The <see cref="ILoggerAdapter"/> instance to which log messages are forwarded. Cannot be <see langword="null"/>.</param>
public class RepeatGuardDecorator(ILoggerAdapter innerLogger) : LoggerDecorator(innerLogger)
{
    private string? _lastMessage;

    private void HandleMessage(string message, Action<string> log)
    {
        if (message == _lastMessage)
        {
            return;
        }

        _lastMessage = message;
        log(message);
    }

    public override void Info(string message) => HandleMessage(message, InnerLogger.Info);
    public override void Warn(string message) => HandleMessage(message, InnerLogger.Warn);
    public override void Error(string message) => HandleMessage(message, InnerLogger.Error);
    public override void Debug(string message) => HandleMessage(message, InnerLogger.Debug);
}