using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace YourTutor.Mvc.Api;

internal static class HealthCheckEndpoint
{
    internal static void AddHealthCheckEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapHealthChecks("api/health", new HealthCheckOptions()
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
    }
}
