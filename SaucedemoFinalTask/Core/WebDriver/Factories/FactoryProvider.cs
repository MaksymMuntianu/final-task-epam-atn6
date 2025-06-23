namespace Core.WebDriver.Factories;

internal static class FactoryProvider
{
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