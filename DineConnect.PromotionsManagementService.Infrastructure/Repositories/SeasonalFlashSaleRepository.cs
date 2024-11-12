using DineConnect.PromotionsManagementService.Application;
using DineConnect.PromotionsManagementService.Domain.Entities;

namespace DineConnect.PromotionsManagementService.Infrastructure.Repositories
{
    public class SeasonalFlashSaleRepository : ISeasonalFlashSaleRepository
    {
        // Simulating data access (can be replaced with actual DB or API call logic)
        public async Task<SeasonalFlashSaleEntity> GetActiveFlashSaleAsync()
        {
            // Example: Business logic to fetch the active flash sale
            var activeSale = new SeasonalFlashSaleEntity
            {
                Id = 1,
                RestaurantId = Guid.Empty,
                DiscountPercentage = 25.0,
                StartDate = DateTime.Now.AddDays(-1),
                EndDate = DateTime.Now.AddDays(10)
            };

            return await Task.FromResult(activeSale);
        }
    }
}
