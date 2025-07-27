using Business.Interfaces.Repository;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection AddProjectRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAuthRepository, AuthRepository>();

            return services;
        }
    }
}
