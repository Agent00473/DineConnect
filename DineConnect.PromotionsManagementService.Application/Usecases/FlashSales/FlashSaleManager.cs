using DineConnect.PromotionsManagementService.Application.Interfaces;
using DineConnect.PromotionsManagementService.Application.Usecases.FlashSales.Interfaces;
using DineConnect.PromotionsManagementService.Domain.FlashSales.Entities;
using DineConnect.PromotionsManagementService.Domain.FlashSales.ValueObjects;

namespace DineConnect.PromotionsManagementService.Application.Usecases.FlashSales
{
    internal class FlashSaleManager : IFlashSaleManager
    {
        private IFlashSaleRepository _flashSaleRepository;
        private IFlashSaleManagerFactory _saleManagerFactory;
        public FlashSaleManager(IFlashSaleRepository repository, IFlashSaleManagerFactory saleManagerFactory)
        {
            _flashSaleRepository = repository;
            _saleManagerFactory = saleManagerFactory;
        }
        public async Task<DiscountData> CalculateDiscountAmount(Guid customerId, double orderAmount, FlashSaleType saleType)
        {
            var customer = await _flashSaleRepository.GetCustomerAsync(customerId);
            IFlashSaleUseCase useCase = _saleManagerFactory.GetFlashSaleManager(saleType);
            DiscountData discount = useCase != null ?
                    await useCase.CalculateDiscountAsync(customer, orderAmount) : new DiscountData(0, string.Empty);
            return discount;
        }

        public async Task<FlashSale> GetFlashSaleByTypeAsync(FlashSaleType saleType)
        {
            var flashSale = await _flashSaleRepository.GetFlashSaleByTypeAsync(saleType);
            return flashSale;
        }
    }
}
