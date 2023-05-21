using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Respawn;
using Respawn.Graph;
using YourTutor.Application.Constants;
using YourTutor.Application.Settings;
using YourTutor.Infrastructure.DAL;
using YourTutor.Tests.Integration.Helpers;

namespace YourTutor.Tests.Integration;

public class YourTutorApp : WebApplicationFactory<Program>, IAsyncLifetime
{
    private string _connectionString;

    private Respawner _respawner;

    public HttpClient Client { get; private set; }
    internal YourTutorDbContext DbContext { get; private set; }

    public YourTutorApp()
    {

    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment(EnvironmentService.TestEnvironment);

        builder.ConfigureTestServices(services =>
        {
            services.AddControllersWithViews(options =>
            {
                options.Filters.Remove(new AutoValidateAntiforgeryTokenAttribute());
            });
        });
    }

    public async Task InitializeAsync()
    {
        Client = CreateClient();
        _connectionString = SettingsHelper.GetSettings<ConnectionStringsSettings>().DefaultConnectionString;
        DbContext = new TestYourTutorDbContext().DbContext;
        await InitializeRespawner();
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await DbContext.Database.EnsureDeletedAsync();
        await DbContext.DisposeAsync();
    }


    private async Task InitializeRespawner()
    {
        _respawner = await Respawner.CreateAsync(_connectionString, new RespawnerOptions()
        {
            SchemasToInclude = new[]
            {
                "dbo"
            },
            TablesToIgnore = new Table[]
            {
                new Table("__EFMigrationsHistory")
            }
        });
    }

    public async Task ResetDbAsync()
    {
        await _respawner.ResetAsync(_connectionString);
    }
}


