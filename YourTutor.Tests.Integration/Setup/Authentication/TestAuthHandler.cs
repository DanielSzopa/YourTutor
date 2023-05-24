using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace YourTutor.Tests.Integration.Setup.Authentication;

public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
        : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {        
        var claims = new List<Claim>();

        if(Context.Request.Headers.TryGetValue(TestAuthClaims.UserId, out var userId))
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, userId[0]));
        }

        var identity = new ClaimsIdentity(claims, TestAuthScheme.Scheme);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, TestAuthScheme.Scheme);

        var result = AuthenticateResult.Success(ticket);

        return Task.FromResult(result);
    }
}
