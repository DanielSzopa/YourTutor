using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Respawn;
using Respawn.Graph;
using YourTutor.Application.Constants;
using YourTutor.Application.Settings;
using YourTutor.Infrastructure.DAL;
using YourTutor.Tests.Integration.Helpers;
using YourTutor.Tests.Integration.Setup.Authentication;

namespace YourTutor.Tests.Integration.Setup;

public class YourTutorApp : WebApplicationFactory<Program>, IAsyncLifetime
{
    private string _connectionString;

    private Respawner _respawner;

    public HttpClient Client { get; private set; }
    public HttpClient AuthenticatedClient { get; private set; }
    public IServiceProvider ServiceProvider { get; private set; }
    internal YourTutorDbContext YourTutorDbContext { get; private set; }
    internal TestDatabase TestDatabase { get; private set; }

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

            ServiceProvider = services.BuildServiceProvider();
        });
    }

    public async Task InitializeAsync()
    {
        _connectionString = SettingsHelper.GetSettings<ConnectionStringsSettings>().DefaultConnectionString;
        TestDatabase = new TestDatabase(_connectionString);
        YourTutorDbContext = TestDatabase.YourTutorDbContext;
        await TestDatabase.InitializeDbAsync();

        InitializeClients();

        await InitializeRespawner();
    }

    private void InitializeClients()
    {
        var clientOptions = new WebApplicationFactoryClientOptions() { AllowAutoRedirect = false };

        Client = CreateClient(clientOptions);

        AuthenticatedClient = WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddTestAuthentication();
            });

        }).CreateClient(clientOptions);
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await TestDatabase.CustomDisposeAsync();
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

    public T GetRequiredService<T>() =>
        ServiceProvider.GetRequiredService<T>();
}


