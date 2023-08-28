using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YourTutor.Application.Abstractions.UnitOfWork;
using YourTutor.Application.Settings;
using YourTutor.Core.Repositories;
using YourTutor.Infrastructure.DAL.Repositories;
using YourTutor.Infrastructure.DAL.Seeds;
using YourTutor.Infrastructure.Settings;

namespace YourTutor.Infrastructure.DAL
{
    internal static class Extensions
    {
        internal static IServiceCollection AddYourTutorDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetSettings<ConnectionStringsSettings>();
            services.AddDbContext<YourTutorDbContext>(x => x.UseSqlServer(connectionString.DefaultConnectionString,
                x => x.MigrationsHistoryTable(ConstantsDAL.MigrationsHistoryTable, ConstantsDAL.DefaultSchema)));

            return services;
        }

        internal static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                    .AddScoped<IUserRepository, UserRepository>()
                    .AddScoped<ITutorRepository, TutorRepository>()
                    .AddScoped<IOfferRepository, OfferRepository>()
                    .AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        }

        internal static IServiceCollection AddSeeders(this IServiceCollection services)
        {
            return services
                    .AddSingleton<IYourTutorSeeder, YourTutorSeeder>();
        }

        internal static IServiceCollection AddDbInitializerHostedService(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetSettings<DbInitializerSettings>().IsEnabled)
                services.AddHostedService<DatabaseInitializer>();

            return services;
        }
    }
}


