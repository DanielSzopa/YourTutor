using Microsoft.AspNetCore.Authorization;
using YourTutor.Application.Abstractions;
using YourTutor.Application.Commands;
using YourTutor.Core.Repositories;
using YourTutor.Core.ValueObjects;

namespace YourTutor.Infrastructure.Authorization;

public sealed class CanRemoveOffertRequirementHandler : AuthorizationHandler<CanRemoveOffertRequirement, DeleteOffert>
{
    private readonly IHttpContextService _httpContextService;
    private readonly IOffertRepository _offertRepository;

    public CanRemoveOffertRequirementHandler(IHttpContextService httpContextService, IOffertRepository offertRepository)
    {
        _httpContextService = httpContextService;
        _offertRepository = offertRepository;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CanRemoveOffertRequirement requirement, DeleteOffert resource)
    {
        var userId = _httpContextService.GetUserIdFromClaims();

        if(userId == Guid.Empty)
        {
            context.Fail();
            return;
        }
       

        if(await _offertRepository.CheckIfUserHasAccessToOffert(new OffertId(resource.Id), new UserId(userId)))
        {
            context.Succeed(requirement);
            return;
        }
    }
}


