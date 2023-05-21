using YourTutor.Application.Abstractions.Settings;

namespace YourTutor.Infrastructure.Settings;

public sealed class DbInitializerSettings : Settings<DbInitializerSettings>, ISettings
{
    public static string SectionName => "DbInitializer";

    public bool IsEnabled { get; set; }
}


