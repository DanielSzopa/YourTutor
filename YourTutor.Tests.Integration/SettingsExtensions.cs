using YourTutor.Application.Abstractions.Settings;

namespace YourTutor.Tests.Integration;

public static class SettingsExtensions
{
    public static TSetting GetSettings<TSetting>(this IConfiguration configuration)
        where TSetting : class, ISettings, new()
    {
        var settings = new TSetting();
        var section = configuration.GetRequiredSection(TSetting.SectionName);
        section.Bind(settings);

        return settings;
    }

    public static string GetSettings(this IConfiguration configuration, string section)
    {
        var config = configuration.GetValue<string>(section);
        if (config is null)
            throw new Exception($"Section: {section} return null config");
        return config;
    }
}
