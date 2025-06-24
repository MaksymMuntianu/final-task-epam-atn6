using Core.Loggers;
using Moq;

namespace Tests.Unit.Core.Loggers;

[TestClass]
public class PrefixDecoratorTests
{
    [DataTestMethod]
    [DataRow(nameof(ILoggerAdapter.Info))]
    [DataRow(nameof(ILoggerAdapter.Warn))]
    [DataRow(nameof(ILoggerAdapter.Error))]
    [DataRow(nameof(ILoggerAdapter.Debug))]
    public void AllLogs_WhenLoggingAtAnyLevel_AddsPrefixToLogMessage(string methodName)
    {
        // Arrange
        var mockLogger = new Mock<ILoggerAdapter>();
        var decorator = new PrefixDecorator(mockLogger.Object, "[PREFIX]");

        const string message = "Test";
        const string expected = "[PREFIX] Test";

        // Act & Assert
        switch (methodName)
        {
            case nameof(ILoggerAdapter.Info):
                decorator.Info(message);
                mockLogger.Verify(l => l.Info(expected), Times.Once(), "Prefix should be added to Info log message.");
                break;
            case nameof(ILoggerAdapter.Warn):
                decorator.Warn(message);
                mockLogger.Verify(l => l.Warn(expected), Times.Once(), "Prefix should be added to Warn log message.");
                break;
            case nameof(ILoggerAdapter.Error):
                decorator.Error(message);
                mockLogger.Verify(l => l.Error(expected), Times.Once(), "Prefix should be added to Error log message.");
                break;
            case nameof(ILoggerAdapter.Debug):
                decorator.Debug(message);
                mockLogger.Verify(l => l.Debug(expected), Times.Once(), "Prefix should be added to Debug log message.");
                break;
        }
    }

    [TestMethod]
    public void Info_PrefixWhitespace_PrefixTrimmed()
    {
        // Arrange
        var mockLogger = new Mock<ILoggerAdapter>();
        var decorator = new PrefixDecorator(mockLogger.Object, " ");

        // Act
        decorator.Info("Information");

        // Assert
        mockLogger.Verify(l => l.Info("Information"), Times.Once(), "Prefix should not affect the log message when it is just whitespace.");
    }

    [TestMethod]
    public void Warn_PrefixEmptyString_PrefixTrimmed()
    {
        // Arrange
        var mockLogger = new Mock<ILoggerAdapter>();
        var decorator = new PrefixDecorator(mockLogger.Object, string.Empty);

        // Act
        decorator.Warn("Information");

        // Assert
        mockLogger.Verify(l => l.Warn("Information"), Times.Once(), "Prefix should not affect the log message when it is an string.Empty.");
    }

    [TestMethod]
    public void Debug_PrefixNull_PrefixTrimmed()
    {
        // Arrange
        var mockLogger = new Mock<ILoggerAdapter>();
        var decorator = new PrefixDecorator(mockLogger.Object, null!);

        // Act
        decorator.Debug("Information");

        // Assert
        mockLogger.Verify(l => l.Debug("Information"), Times.Once(), "Prefix should not affect the log message when it is null.");
    }

    [TestMethod]
    public void Error_MessageNull_OnlyPrefix()
    {
        // Arrange
        var mockLogger = new Mock<ILoggerAdapter>();
        var decorator = new PrefixDecorator(mockLogger.Object, "Prefix: ");

        // Act
        decorator.Error(null!);

        // Assert
        mockLogger.Verify(l => l.Error("Prefix:"), Times.Once(), "If message is null, only the prefix should be logged.");
    }
}