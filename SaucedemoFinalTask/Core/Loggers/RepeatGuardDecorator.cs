namespace Core.Loggers;

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