using DineConnect.PromotionsManagementService.Domain.Entities;

namespace DineConnect.PromotionsManagementService.Application
{
    public interface ISeasonalFlashSaleRepository
    {
        Task<SeasonalFlashSaleEntity> GetActiveFlashSaleAsync();
    }
}
