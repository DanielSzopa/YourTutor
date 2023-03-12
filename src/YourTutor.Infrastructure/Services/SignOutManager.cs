using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using YourTutor.Application.Abstractions.UserManager;

namespace YourTutor.Infrastructure.Services
{
    public sealed class SignOutManager : ISignOutManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SignOutManager(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;
        public async Task SignOutAsync() => await _httpContextAccessor.HttpContext.SignOutAsync();
    }
}


