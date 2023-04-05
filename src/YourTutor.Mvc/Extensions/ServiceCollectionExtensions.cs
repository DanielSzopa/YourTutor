using Microsoft.AspNetCore.Mvc;

namespace YourTutor.Mvc.Extensions
{
    internal static class ServiceCollectionExtensions
    {      
        internal static IServiceCollection AddControllersExtension(this IServiceCollection services)
        {
            services
                .AddControllersWithViews(options =>
                {
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                });

            return services;
        }
    }
}
