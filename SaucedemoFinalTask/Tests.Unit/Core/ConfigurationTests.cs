using Core;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using FluentAssertions;


namespace Tests.Unit.Core
{
    [TestClass]
    public sealed class ConfigurationTests
    {
        [TestMethod]
        public void Configuration_Returns_Default_Values_When_Keys_Missing()
        {
            var inMemoryConfig = new ConfigurationBuilder()
                .AddInMemoryCollection()
                .Build();
            var config = new Configuration(inMemoryConfig);
            
            config.BrowserType.Should().Be("Chrome");
            config.AppUrl.Should().Be(string.Empty);
            config.TestDataPath.Should().Be(string.Empty);
        }

        [TestMethod]
        public void Configuration_Loads_Values_From_InMemory_Configuration()
        {
            var settings = new Dictionary<string, string?>
            {
                { "BrowserType", "Firefox" },
                { "AppUrl", "http://localhost" },
                { "TestDataPath", "/data/test" }
            };
            var inMemoryConfig = new ConfigurationBuilder()
                .AddInMemoryCollection(settings)
                .Build();
            var config = new Configuration(inMemoryConfig);

            config.BrowserType.Should().Be("Firefox");
            config.AppUrl.Should().Be("http://localhost");
            config.TestDataPath.Should().Be("/data/test");
        }

    }
}
