using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Respawn;
using Respawn.Graph;
using YourTutor.Application.Constants;
using YourTutor.Application.Settings;

namespace YourTutor.Tests.Integration;

public class YourTutorApp : WebApplicationFactory<Program>, IAsyncLifetime
{
    private IConfiguration _configuration;

    private string _connectionString;

    private Respawner _respawner;

    public HttpClient Client { get; private set; }
    public IServiceProvider ServiceProvider { get; private set; }

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

        builder.ConfigureAppConfiguration((context, builder) =>
        {
            _configuration = builder.Build();
        });
    }

    public T GetSettings<T>()
        where T : class, Application.Abstractions.Settings.ISettings, new() => 
        _configuration.GetSettings<T>();

    public string GetSettings(string sectionName)
        => _configuration.GetSettings(sectionName);

   
    public async Task InitializeAsync()
    {
        Client = CreateClient();
        _connectionString = GetSettings<ConnectionStringsSettings>().DefaultConnectionString;
        await InitializeRespawner();
    }


    async Task IAsyncLifetime.DisposeAsync()
    {
        await ResetDbAsync();
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


