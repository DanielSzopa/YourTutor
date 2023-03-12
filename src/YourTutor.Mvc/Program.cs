using YourTutor.Application;
using YourTutor.Infrastructure;
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

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
