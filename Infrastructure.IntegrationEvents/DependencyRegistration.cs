using Infrastructure.IntegrationEvents.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IntegrationEvents
{
    public static class DependencyRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
                                  IConfiguration configration)
        {
            services.AddPersistance(configration);
            return services;
        }

        public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IntegrationEventDataContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
