using Microsoft.AspNetCore.Mvc;
using YourTutor.Application;
using YourTutor.Infrastructure;
using YourTutor.Infrastructure.Constans;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services
    .AddApplication()
    .AddInfrastructure()
    .AddHttpContextAccessor()
    .AddControllersWithViews(options =>
    {
        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
    });

services.AddAuthentication()
    .AddCookie(Schemes.IdentityScheme, options =>
    {
        options.Cookie = new CookieBuilder()
        {
            Name = "Identity",
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
