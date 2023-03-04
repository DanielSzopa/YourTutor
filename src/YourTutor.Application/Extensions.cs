using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using YourTutor.Infrastructure.Settings;

namespace YourTutor.Application
{
    public static class Extensions
    {
        private static readonly string _applicationAssemblyName = 
            typeof(Extensions).Assembly.GetName().Name;

        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .RegisterSettings<IdentitySettings>(configuration)
                .RegisterSettings<ConnectionStringsSettings>(configuration)
                .RegisterSettings<SendGridSettings>(configuration)
                .RegisterSettings<EmailSettings>(configuration)
                .AddMediatR(config => config.AsScoped(), Assembly.Load(_applicationAssemblyName));
            return services;
        }
    }
}


