using Microsoft.AspNetCore.Authorization;
using YourTutor.Application.Abstractions;
using YourTutor.Application.Commands.DeleteOffer;
using YourTutor.Core.Repositories;
using YourTutor.Core.ValueObjects;

namespace YourTutor.Infrastructure.Authorization.CanRemoveOffer;

public sealed class CanRemoveOfferRequirementHandler : AuthorizationHandler<CanRemoveOfferRequirement, DeleteOffer>
{
    private readonly IHttpContextService _httpContextService;
    private readonly IOfferRepository _offerRepository;

    public CanRemoveOfferRequirementHandler(IHttpContextService httpContextService, IOfferRepository offerRepository)
    {
        _httpContextService = httpContextService;
        _offerRepository = offerRepository;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CanRemoveOfferRequirement requirement, DeleteOffer resource)
    {
        var userId = _httpContextService.GetUserIdFromClaims();

        if (userId == Guid.Empty)
        {
            context.Fail();
            return;
        }


        if (await _offerRepository.CheckIfUserHasAccessToOffer(new OfferId(resource.Id), new UserId(userId)))
        {
            context.Succeed(requirement);
            return;
        }
    }
}


