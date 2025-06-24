namespace Core.Loggers;

public interface ILoggerAdapter
{
    void Info(string message);
    void Warn(string message);
    void Error(string message);
    void Debug(string message);
}