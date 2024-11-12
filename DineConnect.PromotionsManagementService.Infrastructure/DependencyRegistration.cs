using DineConnect.PromotionsManagementService.Application.Interfaces;
using DineConnect.PromotionsManagementService.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DineConnect.PromotionsManagementService.Infrastructure
{
    public static class DependencyRegistration
    {
        public static IServiceCollection AddPromotionsManagementInfrastructureDependencies(this IServiceCollection services,
                                     IConfiguration configration)
        {
            services.AddPersistance(configration);
            return services;
        }

        private static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IFlashSaleRepository, FlashSaleRepository>();
            return services;
        }
    }
}
