using DineConnect.PromotionsManagementService.Application.Usecases.FlashSales.Interfaces;
using DineConnect.PromotionsManagementService.Domain.FlashSales.Entities;

namespace DineConnect.PromotionsManagementService.Application.Usecases.FlashSales.Implementations
{
    internal class HolidayFlashSaleUseCase : IFlashSaleUseCase
    {
        public string SaleName => "Holiday Flash Sale";

        public Task<DiscountData> CalculateDiscountAsync(Customer customer, double orderAmount)
        {
            double discountPercentage = 0.15; 
            return Task.FromResult(new DiscountData(orderAmount * discountPercentage, $"{SaleName} {discountPercentage}% Discount Applied"));
        }
    }
}
