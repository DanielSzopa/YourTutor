using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace YourTutor.Application
{
    public static class Extensions
    {
        private static readonly string _applicationAssemblyName = 
            typeof(Extensions).Assembly.GetName().Name;

        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services              
                .AddMediatR(config => config.AsScoped(), Assembly.Load(_applicationAssemblyName));
            return services;
        }
    }
}


