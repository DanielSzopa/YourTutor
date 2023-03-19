using YourTutor.Application;
using YourTutor.Application.Abstractions;
using YourTutor.Infrastructure;
using YourTutor.Infrastructure.Logging;
using YourTutor.Mvc.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

services
    .AddApplication()
    .AddInfrastructure(config)
    .AddHttpContextAccessor()
    .AddAuthenticationExtension(config)
    .AddControllersExtension();

var app = builder.Build();
var logger = app.Services.GetLogger<Program>();
using var scope = app.Services.CreateAsyncScope();
var clock = scope.ServiceProvider.GetRequiredService<IClock>();

if (app.Environment.IsDevelopment())
{
    app.UseCustomExceptionHandler(logger, clock);
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
