using Microsoft.Extensions.DependencyInjection;
using YourTutor.Infrastructure.Authorization.CanEditTutor;
using YourTutor.Infrastructure.Authorization.CanRemoveOffer;

namespace YourTutor.Infrastructure.Authorization;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(CustomAuthorizationPolicy.DeleteOffer, policy =>
            {
                policy.Requirements.Add(new CanRemoveOfferRequirement());

            });

            options.AddPolicy(CustomAuthorizationPolicy.EditTutor, policy =>
            {
                policy.Requirements.Add(new CanEditTutorRequirement());

            });
        });

        return services;
    }
}


