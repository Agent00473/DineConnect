using DineConnect.PromotionsManagementService.Domain.Entities;

namespace DineConnect.PromotionsManagementService.Application.Usecases
{
    public interface ISeasonalFlashSaleService
    {
        Task<SeasonalFlashSaleEntity> GetActiveFlashSaleAsync();
    }
}
