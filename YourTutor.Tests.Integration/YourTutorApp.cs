using Microsoft.AspNetCore.Mvc.Testing;
using Respawn;

namespace YourTutor.Tests.Integration;

public class YourTutorApp : WebApplicationFactory<Program>, IAsyncLifetime
{
    public HttpClient Client { get; private set; }

    private string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=YourTutor-Test-Db;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

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
                "public"
            }
        });
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await ResetDbAsync();
    }
}


