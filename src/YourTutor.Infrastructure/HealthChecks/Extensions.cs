using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YourTutor.Application.Settings;

namespace YourTutor.Infrastructure.HealthChecks;

public static class Extensions
{
    public static IServiceCollection AddInfrastructureHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        var apiSettings = configuration.GetSettings<SendGridSettings>();
        var connectionStringsSettings = configuration.GetSettings<ConnectionStringsSettings>();

        services.
            AddHealthChecks()
            .AddSqlServer(connectionStringsSettings.DefaultConnectionString)
            .AddSendGrid(apiSettings.ApiKey);

        return services;
    }
}


