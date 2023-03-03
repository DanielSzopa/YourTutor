using Microsoft.Extensions.DependencyInjection;
using YourTutor.Core.Abstractions;
using YourTutor.Core.Abstractions.Repositories;
using YourTutor.Core.Services.SignInManager;
using YourTutor.Infrastructure.DAL;
using YourTutor.Infrastructure.DAL.Repositories;

namespace YourTutor.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services
                .AddScoped<IUserRepository,UserRepository>()
                .AddScoped<ISignInManager, SignInManager>()
                .AddYourTutorDbContext();

            return services;
        }
    }
}


