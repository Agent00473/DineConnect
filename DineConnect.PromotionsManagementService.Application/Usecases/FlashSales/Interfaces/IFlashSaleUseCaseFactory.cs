using DineConnect.PromotionsManagementService.Domain.FlashSales.ValueObjects;


namespace DineConnect.PromotionsManagementService.Application.Usecases.FlashSales.Interfaces
{
    internal interface IFlashSaleManagerFactory
    {
        IFlashSaleUseCase GetFlashSaleManager(FlashSaleType saleType);
    }
}
