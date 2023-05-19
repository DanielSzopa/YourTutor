using Microsoft.AspNetCore.Mvc.Testing;

namespace YourTutor.Tests.Integration;

internal sealed class YourTutorApp : WebApplicationFactory<Program>
{
    public HttpClient Client { get; }
    public YourTutorApp()
    {
        Client = WithWebHostBuilder(builder =>
        {

        })
            .CreateClient();
    }
}


