using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using YourTutor.Core.Abstractions;
using YourTutor.Infrastructure.Constans;

namespace YourTutor.Core.Services.SignInManager
{
    internal sealed class SignInManager : ISignInManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SignInManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task SignInAsync(bool isPersistent, Guid userId)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            };

            var claimIdentity = new ClaimsPrincipal(new ClaimsIdentity(claims, Schemes.IdentityScheme));

            var authProps = new AuthenticationProperties()
            {
                IsPersistent = isPersistent,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(10),
            };

            await _httpContextAccessor.HttpContext.SignInAsync(Schemes.IdentityScheme, claimIdentity, authProps);
        }
    }
}


