namespace Core.Loggers;

/// <summary>
/// Defines a logging interface for writing log messages at various severity levels,
/// and allows implement the adapter pattern.
/// </summary>
public interface ILoggerAdapter
{
    void Info(string message);
    void Warn(string message);
    void Error(string message);
    void Debug(string message);
}