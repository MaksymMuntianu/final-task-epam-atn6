using Core;
using FluentAssertions;
using Microsoft.Extensions.Configuration;

namespace Tests.Unit.Core;

[TestClass]
public sealed class ConfigurationTests
{
    [TestMethod]
    public void Constructor_WhenKeysMissing_InitializesDefaultValues()
    {
        // Arrange
        var inMemoryConfig = new ConfigurationBuilder()
            .AddInMemoryCollection()
            .Build();

        // Act
        var config = new Configuration(inMemoryConfig);

        // Assert
        config.BrowserType.Should().Be("Chrome");
        config.AppUrl.Should().Be("https://www.saucedemo.com/");
    }

    [TestMethod]
    public void Constructor_WithInMemoryConfiguration_InitializesPropertiesFromValues()
    {
        // Arrange
        var settings = new Dictionary<string, string?>
        {
            { "BrowserType", "Firefox" },
            { "AppUrl", "http://localhost" }
        };
        var inMemoryConfig = new ConfigurationBuilder()
            .AddInMemoryCollection(settings)
            .Build();

        // Act
        var config = new Configuration(inMemoryConfig);

        // Assert
        config.BrowserType.Should().Be("Firefox");
        config.AppUrl.Should().Be("http://localhost");
    }
}