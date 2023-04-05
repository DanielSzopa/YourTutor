using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;

namespace YourTutor.Infrastructure.Middlewares;

public static class CustomExceptionMiddleware
{
    public static WebApplication UseCustomExceptionHandler(this WebApplication app)
    {
        var errorHandlerOptions = new ExceptionHandlerOptions()
        {
            ExceptionHandler = httpContext =>
            {
                var feature = httpContext.Features.Get<IExceptionHandlerFeature>();
                var error = feature?.Error;
                httpContext.Response.Redirect("/Home/Error");
                return Task.CompletedTask;
            },
        };
        app.UseExceptionHandler(errorHandlerOptions);

        return app;
    }
}


