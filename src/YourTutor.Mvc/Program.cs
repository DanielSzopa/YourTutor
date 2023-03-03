using YourTutor.Application;
using YourTutor.Infrastructure;
using YourTutor.Infrastructure.Constans;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services
    .AddApplication()
    .AddInfrastructure()
    .AddHttpContextAccessor()
    .AddControllersWithViews();

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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
