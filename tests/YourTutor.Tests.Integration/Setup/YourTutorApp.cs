using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
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
    private readonly string _connectionString = SettingsHelper.GetSettings<ConnectionStringsSettings>().DefaultConnectionString;

    private Respawner _respawner;

    private IServiceProvider _serviceProvider;

    public HttpClient Client { get; private set; }
    public HttpClient AuthenticatedClient { get; private set; }  
    internal TestDatabase TestDatabase { get; private set; }

    private readonly IContainer _testContainer = new ContainerBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2019-latest")
        .WithEnvironment("ACCEPT_EULA", "Y")
        .WithEnvironment("MSSQL_SA_PASSWORD", "-Test123-")
        .WithEnvironment("MSSQL_PID", "Express")
        .WithPortBinding("1433", "1433")
        .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(1433))
        .Build();

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

            _serviceProvider = services.BuildServiceProvider();
        });
    }

    public T GetRequiredService<T>() =>
        _serviceProvider.GetRequiredService<T>();

    public async Task InitializeAsync()
    {
        await _testContainer.StartAsync();
        TestDatabase = new TestDatabase(_connectionString);
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
        await _testContainer.StopAsync();
    }


    private async Task InitializeRespawner()
    {
        _respawner = await Respawner.CreateAsync(_connectionString, new RespawnerOptions()
        {
            SchemasToInclude = new[]
            {
                ConstantsDAL.DefaultSchema
            },
            TablesToIgnore = new Table[]
            {
                new Table(ConstantsDAL.MigrationsHistoryTable)
            }
        });
    }

    public async Task ResetDbAsync()
    {
        await _respawner.ResetAsync(_connectionString);
    }
}


