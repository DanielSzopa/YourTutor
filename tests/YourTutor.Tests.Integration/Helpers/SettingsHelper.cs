using YourTutor.Application.Abstractions.Settings;
using YourTutor.Application.Constants;

namespace YourTutor.Tests.Integration.Helpers;

public static class SettingsHelper
{
    private const string AppSettings = $"appsettings.{EnvironmentService.TestEnvironment}.json";

    public static IConfigurationRoot GetConfigurationRoot()
        => new ConfigurationBuilder()
        .AddJsonFile(AppSettings, false, false)
        .AddEnvironmentVariables()
        .Build();

    public static TSetting GetSettings<TSetting>()
        where TSetting : class, ISettings, new()
    {
        var configuration = GetConfigurationRoot();
        var settings = new TSetting();
        var section = configuration.GetRequiredSection(TSetting.SectionName);
        section.Bind(settings);

        return settings;
    }

    public static string GetSettings(string section)
    {
        var configuration = GetConfigurationRoot();
        var config = configuration.GetValue<string>(section);
        if (config is null)
            throw new Exception($"Section: {section} return null config");
        return config;
    }
}
