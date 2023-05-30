using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace YourTutor.Tests.Integration.Setup.Authentication;

public static class TestAuthExtensions
{
    public static IServiceCollection AddTestAuthentication(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder(TestAuthScheme.Scheme)
                .RequireAuthenticatedUser()
                .Build();
        });

        services.AddAuthentication(TestAuthScheme.Scheme)
            .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(TestAuthScheme.Scheme, options => { });

        return services;
    }

    public static HttpClient AddUserIdClaimHeader(this HttpClient client, string userId)
    {
        client.DefaultRequestHeaders.Add(TestAuthClaims.UserId, userId);
        return client;
    }

    public static HttpClient CleanClaimHeaders(this HttpClient client)
    {
        client.DefaultRequestHeaders.Remove(TestAuthClaims.UserId);
        return client;
    }
}
