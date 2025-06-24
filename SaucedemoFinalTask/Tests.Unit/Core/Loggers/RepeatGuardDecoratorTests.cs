using Core.Loggers;
using Moq;

namespace Tests.Unit;

[TestClass]
public class RepeatGuardDecoratorTests
{

    [TestMethod]
    public void Info_WhenMessageIsUnique_LogsMessage()
    {
        // Arrange
        var mockLogger = new Mock<ILoggerAdapter>();
        var repeatGuard = new RepeatGuardDecorator(mockLogger.Object);

        // Act
        repeatGuard.Info("Message");

        // Assert
        mockLogger.Verify(l => l.Info("Message"), Times.Once);
    }

    [TestMethod]
    public void Warn_WhenCalledWithSameMessageTwice_LogsOnlyOnce()
    {
        // Arrange
        var mockLogger = new Mock<ILoggerAdapter>();
        var repeatGuard = new RepeatGuardDecorator(mockLogger.Object);

        // Act
        repeatGuard.Warn("Repeat me");
        repeatGuard.Warn("Repeat me");

        // Assert
        mockLogger.Verify(l => l.Warn("Repeat me"), Times.Once);
    }

    [TestMethod]
    public void Debug_WhenMessagesChangeAndDuplicate_LogsEachUniqueMessageOnce()
    {
        // Arrange
        var mockLogger = new Mock<ILoggerAdapter>();
        var repeatGuard = new RepeatGuardDecorator(mockLogger.Object);

        // Act
        repeatGuard.Debug("First");
        repeatGuard.Debug("Second");
        repeatGuard.Debug("Second");

        // Assert
        mockLogger.Verify(l => l.Debug("First"), Times.Once);
        mockLogger.Verify(l => l.Debug("Second"), Times.Once); // Only once
    }

    [TestMethod]
    public void Error_WhenSameMessageIsInterruptedByAnother_LogsAgain()
    {
        // Arrange
        var mockLogger = new Mock<ILoggerAdapter>();
        var repeatGuard = new RepeatGuardDecorator(mockLogger.Object);

        // Act
        repeatGuard.Error("Error");
        repeatGuard.Error("Error");
        repeatGuard.Error("Other error");
        repeatGuard.Error("Error");

        // Assert
        mockLogger.Verify(l => l.Error("Error"), Times.Exactly(2));
        mockLogger.Verify(l => l.Error("Other error"), Times.Once);
    }
}
