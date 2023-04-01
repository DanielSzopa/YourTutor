using Serilog;
using YourTutor.Application;
using YourTutor.Infrastructure;
using YourTutor.Mvc.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseLogger();

var services = builder.Services;
var config = builder.Configuration;

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(config)
    .CreateLogger();

var logger = Log.Logger;

try
{
    logger.Information("Start building application");

    services
    .AddApplication()
    .AddInfrastructure(config)
    .AddHttpContextAccessor()
    .AddAuthenticationExtension(config)
    .AddControllersExtension();

    var app = builder.Build();

    logger.Information("Application has been build");

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

    logger.Information("Application works correctly before Run Middleware");

    app.Run();

}
catch (Exception ex)
{
    logger.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
