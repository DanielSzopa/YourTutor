using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;

namespace YourTutor.Tests.Integration;

internal sealed class YourTutorApp : WebApplicationFactory<Program>, IAsyncLifetime
{
    public HttpClient Client { get; private set; }
    public YourTutorApp()
    {
       
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(log => log.ClearProviders());
        builder.UseEnvironment("Test");
    }

    public async Task InitializeAsync()
    {
        Client = CreateClient();
    }

    Task IAsyncLifetime.DisposeAsync()
    {
        return Task.CompletedTask;
    }
}


