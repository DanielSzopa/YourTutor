using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace YourTutor.Infrastructure.Logging
{
    public static class LoggerExtension
    {
        public static ILogger<T> GetLogger<T>(this IServiceProvider serviceProvider)
            where T : class 
            => serviceProvider.GetRequiredService<ILogger<T>>();
    }
}


