using Microsoft.AspNetCore.Diagnostics;
using YourTutor.Application.Abstractions;

namespace YourTutor.Mvc.Extensions
{
    internal static class WebApplicationExtensions
    {
        internal static WebApplication UseCustomExceptionHandler(this WebApplication app, ILogger logger, IClock clock)
        {
            var errorHandlerOptions = new ExceptionHandlerOptions()
            {
                ExceptionHandler = httpContext =>
                {
                    var feature = httpContext.Features.Get<IExceptionHandlerFeature>();
                    var error = feature?.Error;
                    logger.LogCritical("Unexpected error: {@date}", clock.Now);
                    httpContext.Response.Redirect("/Home/Error");
                    return Task.CompletedTask;
                },
            };
            app.UseExceptionHandler(errorHandlerOptions);

            return app;
        }
    }
}
