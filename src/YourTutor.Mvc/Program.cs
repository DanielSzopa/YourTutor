using Microsoft.AspNetCore.Mvc;
using YourTutor.Application;
using YourTutor.Infrastructure.Constans;
using YourTutor.Infrastructure.Extensions;
using YourTutor.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

services
    .AddApplication()
    .AddInfrastructure(config)
    .AddHttpContextAccessor()
    .AddControllersWithViews(options =>
    {
        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
    });

var identitySettings = services.GetSettings<IdentitySettings>(config);

services.AddAuthentication()
    .AddCookie(Schemes.IdentityScheme, options =>
    {
        options.Cookie = new CookieBuilder()
        {
            Name = identitySettings.CookieName,
            HttpOnly = true,
            SecurePolicy = CookieSecurePolicy.Always
        };
        options.LoginPath = "/Account/Login";
    });
    

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

await app.Services.UseInfrastructure();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
