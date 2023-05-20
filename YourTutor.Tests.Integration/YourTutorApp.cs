using Microsoft.AspNetCore.Mvc.Testing;
using Respawn;
using Respawn.Graph;

namespace YourTutor.Tests.Integration;

public class YourTutorApp : WebApplicationFactory<Program>, IAsyncLifetime
{
    private IConfiguration _configuration;
    public HttpClient Client { get; private set; }

    private readonly string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=YourTutor-Test-Db;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

    private Respawner _respawner;
    public YourTutorApp()
    {
        
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test");

        builder.ConfigureServices(services =>
        {

        });

        builder.ConfigureAppConfiguration((context, builder) =>
        {
            _configuration = builder.Build();
        });
    }

    public async Task ResetDbAsync()
    {
        await _respawner.ResetAsync(_connectionString);
    }


    public async Task InitializeAsync()
    {
        Client = CreateClient();
        await InitializeRespawner();
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

    async Task IAsyncLifetime.DisposeAsync()
    {
        await ResetDbAsync();
    }
}


