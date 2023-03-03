using Microsoft.Extensions.DependencyInjection;
using YourTutor.Infrastructure.DAL;

namespace YourTutor.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddYourTutorDbContext();

            return services;
        }
    }
}


