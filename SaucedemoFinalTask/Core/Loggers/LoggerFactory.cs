namespace Core.Loggers;

/// <summary>
/// Provides a factory for creating logger instances based on the specified logger type.
/// </summary>
/// <remarks>This class simplifies the creation of loggers by abstracting the instantiation of specific logger
/// implementations. Use the <see cref="CreateLogger"/> method to obtain an instance of a logger adapter corresponding
/// to the desired logging framework.</remarks>
public class LoggerFactory
{
    /// <summary>Creates an instance of an <see cref="ILoggerAdapter"/> based on the specified logger type.</summary>
    /// <param name="loggerType">The type of logger to create. Must be a valid <see cref="LoggerType"/> value.</param>
    /// <returns>An <see cref="ILoggerAdapter"/> implementation corresponding to the specified <paramref name="loggerType"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the <paramref name="loggerType"/> is not a recognized <see cref="LoggerType"/> value.</exception>
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