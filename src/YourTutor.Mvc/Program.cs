using Serilog;
using YourTutor.Application;
using YourTutor.Application.Helpers;
using YourTutor.Infrastructure;
using YourTutor.Infrastructure.Middlewares;
using YourTutor.Mvc.Api;
using YourTutor.Mvc.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseLogger();

var services = builder.Services;
var config = builder.Configuration;

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(config)
    .CreateLogger();

var logger = Log.Logger;
var logEvent = new
{
    EventId = AppLogEvent.Start
};

try
{
    logger.Information("Start building application, {0}", logEvent);

    services
    .AddApplication()
    .AddInfrastructure(config)
    .AddControllersExtension();

    var app = builder.Build();

    logger.Information("Application has been build, {0}", logEvent);

    if (!app.Environment.IsDevelopment())
    {
        app.UseCustomExceptionHandler();
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.AddHealthCheckEndpoint();

    logger.Information("Application works correctly before Run Middleware, {0}", logEvent);

    app.Run();

}
catch (Exception ex)
{
    logger.Fatal(ex, "Application terminated unexpectedly, {0}", logEvent);
}
finally
{
    Log.CloseAndFlush();
}

public partial class Program { }