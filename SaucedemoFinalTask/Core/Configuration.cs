using Microsoft.Extensions.Configuration;

namespace Core;

public class Configuration(IConfiguration configuration)
{
    public Configuration() : this(new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", true, true)
        .Build())
    {
    }

    public string BrowserType { get; } = configuration["BrowserType"] ?? "Chrome";

    public string AppUrl { get; } = configuration["AppUrl"] ?? "https://www.saucedemo.com/";
}