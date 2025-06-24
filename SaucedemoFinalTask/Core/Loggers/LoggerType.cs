namespace Core.Loggers;

/// <summary>Specifies the type of logging framework to be used.</summary>
/// <remarks>This enumeration is used to indicate the logging framework that the application should utilize. It
/// currently supports <see cref="LoggerType.NLog"/> and <see cref="LoggerType.Log4Net"/>.</remarks>
public enum LoggerType
{
    NLog,
    Log4Net
}