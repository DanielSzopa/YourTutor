using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace YourTutor.Infrastructure.Logging;

public static class Extensions
{
    public static ILogger<T> GetLogger<T>(this WebApplicationBuilder builder)
        where T : class
    {
        var loggerFactory = CustomLoggerFactory
            .Create(builder.Configuration);

        return loggerFactory
            .CreateLogger<T>();
    }

    internal static IServiceCollection AddCustomLogging(this IServiceCollection services, IConfiguration configuration)
    {
        var loggerFactory = CustomLoggerFactory
            .Create(configuration);

        services.AddSingleton(loggerFactory);
        return services;
    }
}
