using Application.Services;
using Business.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
