using YourTutor.Application;
using YourTutor.Application.Helpers;
using YourTutor.Infrastructure;
using YourTutor.Infrastructure.Logging;
using YourTutor.Infrastructure.Middlewares;
using YourTutor.Mvc.Api;
using YourTutor.Mvc.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();

var services = builder.Services;
var config = builder.Configuration;

var logger = builder
    .GetLogger<Program>();

try
{
    logger.LogInformation(AppLogEvent.Start, "Start building application");

    services
    .AddApplication()
    .AddInfrastructure(config)
    .AddControllersExtension();

    var app = builder.Build();

    logger.LogInformation(AppLogEvent.Start, "Application has been build");

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

    logger.LogInformation(AppLogEvent.Start, "Application works correctly before Run Middleware");

    app.Run();

}
catch (Exception ex)
{
    logger.LogCritical(AppLogEvent.Start, ex, "Application terminated unexpectedly");
}

public partial class Program { }