using DineConnect.PromotionsManagementService.Application.Usecases.FlashSales.Interfaces;
using DineConnect.PromotionsManagementService.Domain.FlashSales.Entities;

namespace DineConnect.PromotionsManagementService.Application.Usecases.FlashSales.Implementations
{
    internal class MysteryFlashSaleUseCase : IFlashSaleUseCase
    {
        public string SaleName => "Mystery Flash Sale";

        public  Task<DiscountData> CalculateDiscountAsync(Customer customer, double orderAmount)
        {
            // Randomly choose a discount percentage between 5% and 20%
            Random random = new Random();
            double discountPercentage = random.Next(5, 21) / 100.0;
            return Task.FromResult(new DiscountData(orderAmount * discountPercentage, $"{SaleName} {discountPercentage}% Discount Applied"));
        }
    }
}
