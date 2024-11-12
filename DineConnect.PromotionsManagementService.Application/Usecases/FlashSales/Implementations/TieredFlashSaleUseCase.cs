using DineConnect.PromotionsManagementService.Application.Usecases.FlashSales.Interfaces;
using DineConnect.PromotionsManagementService.Domain.FlashSales.Entities;

namespace DineConnect.PromotionsManagementService.Application.Usecases.FlashSales.Implementations
{
    internal class TieredFlashSaleUseCase : IFlashSaleUseCase
    {
        public string SaleName => "Tiered Flash Sale";

        public Task<DiscountData> CalculateDiscountAsync(Customer customer, double orderAmount)
        {
            double discountAmount = 0;

            if (orderAmount >= 200)
                discountAmount = 20;
            else if (orderAmount >= 100)
                discountAmount = 10;

            return Task.FromResult( new DiscountData(discountAmount, $"{SaleName} Discount Applied"));
        }

    }
}
