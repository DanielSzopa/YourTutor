using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YourTutor.Infrastructure.Extensions;
using YourTutor.Infrastructure.Settings;

namespace YourTutor.Infrastructure.DAL
{
    internal static class Extensions
    {
        internal static IServiceCollection AddYourTutorDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = services.GetSettings<ConnectionStringsSettings>(configuration);
            services.AddDbContext<YourTutorDbContext>(x => x.UseSqlServer(connectionString.DefaultConnectionString));

            return services;
        }
    }
}


