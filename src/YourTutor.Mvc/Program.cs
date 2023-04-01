using Serilog;
using YourTutor.Application;
using YourTutor.Application.Abstractions;
using YourTutor.Infrastructure;
using YourTutor.Infrastructure.Logging;
using YourTutor.Mvc.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});

var services = builder.Services;
var config = builder.Configuration;

services
    .AddApplication()
    .AddInfrastructure(config)
    .AddHttpContextAccessor()
    .AddAuthenticationExtension(config)
    .AddControllersExtension();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseCustomExceptionHandler(app.Logger);
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

app.Run();
