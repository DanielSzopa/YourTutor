using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace YourTutor.Infrastructure.Logging;

internal class CustomLoggerFactory
{
    private static ILoggerFactory LoggerFactoryInstance;
    private CustomLoggerFactory()
    {
        
    }

    internal static ILoggerFactory Create(IConfiguration configuration)
    {
        if (LoggerFactoryInstance is null)
        {
            var loggerFactory = LoggerFactory.Create(logger =>
            {
                var serilogLogger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Filter.ByExcluding(logEvent =>
                {
                    return logEvent.Exception is OperationCanceledException;
                })
                .CreateLogger();

                logger.AddSerilog(serilogLogger, true);
            });

            LoggerFactoryInstance = loggerFactory;
        }

        return LoggerFactoryInstance;
    }
}
