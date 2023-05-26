using Microsoft.AspNetCore.Authorization;
using YourTutor.Application.Abstractions;

namespace YourTutor.Infrastructure.Authorization;

public class CanEditTutorRequirementHandler : AuthorizationHandler<CanEditTutorRequirement, CanEditTutorRequest>
{
    private readonly IHttpContextService _httpContextService;

    public CanEditTutorRequirementHandler(IHttpContextService httpContextService)
    {
        _httpContextService = httpContextService;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CanEditTutorRequirement requirement, CanEditTutorRequest resource)
    {
        var userId = _httpContextService.GetUserIdFromClaims();

        if (userId == Guid.Empty || resource.TutorId != userId)
        {
            context.Fail();
            return Task.CompletedTask;
        }

        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}


