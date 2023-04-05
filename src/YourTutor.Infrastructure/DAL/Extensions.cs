using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YourTutor.Application.Abstractions.UnitOfWork;
using YourTutor.Application.Settings;
using YourTutor.Core.Repositories;
using YourTutor.Infrastructure.DAL.Repositories;

namespace YourTutor.Infrastructure.DAL
{
    internal static class Extensions
    {
        internal static IServiceCollection AddYourTutorDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetSettings<ConnectionStringsSettings>();
            services.AddDbContext<YourTutorDbContext>(x => x.UseSqlServer(connectionString.DefaultConnectionString));

            return services;
        }

        internal static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                    .AddScoped<IUserRepository, UserRepository>()
                    .AddScoped<ITutorRepository, TutorRepository>()
                    .AddScoped<IOffertRepository, OffertRepository>()
                    .AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        }
    }
}


