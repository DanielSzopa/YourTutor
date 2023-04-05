using System.Security.Claims;

namespace YourTutor.Application.Abstractions
{
    public interface IHttpContextService
    {
        Guid GetUserIdFromClaims();

        ClaimsPrincipal GetUser();
    }
}
