using DineConnect.PromotionsManagementService.Application.Usecases.FlashSales.Interfaces;
using DineConnect.PromotionsManagementService.Domain.FlashSales.Entities;
using DineConnect.PromotionsManagementService.Domain.FlashSales.ValueObjects;

namespace DineConnect.PromotionsManagementService.Application.Usecases.FlashSales.Implementations
{
    internal class CategorySpecificFlashSaleUseCase : IFlashSaleUseCase
    {
        public string SaleName => "Category-Specific Flash Sale";
        private readonly ProductCategory[] _targetCategory;
        private readonly double _categoryDiscountPercentage;

        public CategorySpecificFlashSaleUseCase(ProductCategory[] targetCategory, double discountPercentage)
        {
            _targetCategory = targetCategory;
            _categoryDiscountPercentage = discountPercentage;
        }

        public Task<DiscountData> CalculateDiscountAsync(Customer customer, double orderAmount)
        {
            double discountAmount = 0;
            string message = "No discount available.";

            if (customer != null)
            {
                // Sample discount logic based on customer type and order amount
                if (customer.Type == CustomerType.Premium)
                {
                    discountAmount = orderAmount * 0.15; // 15% for premium customers
                    message = "Premium customer discount applied.";
                }
                else if (customer.Type == CustomerType.Regular)
                {
                    discountAmount = orderAmount * 0.10; // 10% for regular customers
                    message = "Regular customer discount applied.";
                }

                if (orderAmount >= 100)
                {
                    discountAmount += 10; // Flat $10 off on orders over $100
                }

            }
            return Task.FromResult(new DiscountData(discountAmount, $"{SaleName} {message}"));

        }
    }
}
