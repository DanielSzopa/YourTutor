using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace YourTutor.Infrastructure.Logging
{
    internal static class LoggerExtension
    {
        internal static ILogger<T> GetLogger<T>(this IServiceProvider serviceProvider)
            where T : class 
            => serviceProvider.GetRequiredService<ILogger<T>>();
    }
}


