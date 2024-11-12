using DineConnect.PromotionsManagementService.Application.Usecases.FlashSales.Interfaces;
using DineConnect.PromotionsManagementService.Domain.FlashSales.ValueObjects;
using Microsoft.Extensions.DependencyInjection;

namespace DineConnect.PromotionsManagementService.Application.Usecases.FlashSales.Implementations
{
    internal class FlashSaleUseCaseFactory : IFlashSaleManagerFactory
    {

        private readonly IServiceProvider _serviceProvider;

        public FlashSaleUseCaseFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IFlashSaleUseCase GetFlashSaleManager(FlashSaleType saleType)
        {
            IFlashSaleUseCase useCase = null;

            switch (saleType)
            {
                case FlashSaleType.Tiered:
                    useCase = _serviceProvider.GetRequiredService<TieredFlashSaleUseCase>();
                    break;
                case FlashSaleType.Holiday:
                    useCase = _serviceProvider.GetRequiredService<HolidayFlashSaleUseCase>();
                    break;
                case FlashSaleType.Mystery:
                    useCase = _serviceProvider.GetRequiredService<MysteryFlashSaleUseCase>();
                    break;
                case FlashSaleType.ProductCategorySpecific:
                    useCase = _serviceProvider.GetRequiredService<CategorySpecificFlashSaleUseCase>();
                    break;
                    // Add more cases for other flash sale types
            }
            return useCase;
        }
    }
}
