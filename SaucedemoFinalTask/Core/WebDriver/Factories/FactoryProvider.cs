namespace Core.WebDriver.Factories;

/// <summary>Provides a factory instance for creating browser-specific test objects.</summary>
internal static class FactoryProvider
{
    /// <summary>Gets a factory instance for the specified browser type.</summary>
    public static ITestFactory GetFactory(BrowserType browserType)
    {
        return browserType switch
        {
            BrowserType.Chrome => new ChromeTestFactory(),
            BrowserType.Edge => new EdgeTestFactory(),
            _ => throw new NotSupportedException($"Browser type \"{browserType}\" is not supported.")
        };
    }
}