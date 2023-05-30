using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using YourTutor.Application.Abstractions.UserManager;
using YourTutor.Application.Settings;
using YourTutor.Infrastructure.Authentication;

namespace YourTutor.Core.Services.SignInManager
{
    internal sealed class SignInManager : ISignInManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IdentitySettings _identitySettings;

        public SignInManager(IHttpContextAccessor httpContextAccessor, IOptions<IdentitySettings> identitySettings)
        {
            _httpContextAccessor = httpContextAccessor;
            _identitySettings = identitySettings.Value;
        }

        public async Task SignInAsync(bool isPersistent, Guid userId, string fullName)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Name, fullName)
            };

            var claimIdentity = new ClaimsPrincipal(new ClaimsIdentity(claims, AuthenticationSchemes.IdentityScheme));

            var authProps = new AuthenticationProperties()
            {
                IsPersistent = isPersistent,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(_identitySettings.ExpiresDays),
            };

            await _httpContextAccessor.HttpContext.SignInAsync(AuthenticationSchemes.IdentityScheme, claimIdentity, authProps);
        }
    }
}


