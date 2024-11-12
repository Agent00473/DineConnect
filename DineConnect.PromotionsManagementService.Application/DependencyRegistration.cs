using DineConnect.PromotionsManagementService.Application.Usecases.FlashSales;
using DineConnect.PromotionsManagementService.Application.Usecases.FlashSales.Implementations;
using DineConnect.PromotionsManagementService.Application.Usecases.FlashSales.Interfaces;
using DineConnect.PromotionsManagementService.Domain.FlashSales.ValueObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DineConnect.PromotionsManagementService.Application
{
    public static class DependencyRegistration
    {
        public static IServiceCollection AddPromotionsManagementApplicationDependencies(this IServiceCollection services,
                                     IConfiguration configration)
        {
            services.AddScoped(provider =>
                        new CategorySpecificFlashSaleUseCase([ProductCategory.Electronics], 15));
            services.AddScoped<HolidayFlashSaleUseCase>();
            services.AddScoped<MysteryFlashSaleUseCase>();
            services.AddScoped<TieredFlashSaleUseCase>();


            // Register the factory that will resolve the correct IFlashSaleManager
            services.AddScoped<IFlashSaleManagerFactory, FlashSaleUseCaseFactory>();

            services.AddScoped<IFlashSaleManager, FlashSaleManager>();

            return services;
        }

    }
}
