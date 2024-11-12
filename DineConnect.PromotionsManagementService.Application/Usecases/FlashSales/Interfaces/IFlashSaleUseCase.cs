using DineConnect.PromotionsManagementService.Domain.FlashSales.Entities;

namespace DineConnect.PromotionsManagementService.Application.Usecases.FlashSales.Interfaces
{
    internal interface IFlashSaleUseCase
    {
        Task<DiscountData> CalculateDiscountAsync(Customer customer, double orderAmount);
        string SaleName { get; }
    }
}
