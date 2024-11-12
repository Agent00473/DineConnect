
using DineConnect.PromotionsManagementService.Domain.Entities;

namespace DineConnect.PromotionsManagementService.Application.Usecases.Implementations
{
    public class SeasonalFlashSaleServiceImplementation : ISeasonalFlashSaleService
    {
        private readonly ISeasonalFlashSaleRepository _flashSaleRepository;

        // Injecting repository or any required dependencies
        public SeasonalFlashSaleServiceImplementation(ISeasonalFlashSaleRepository flashSaleRepository)
        {
            _flashSaleRepository = flashSaleRepository;
        }

        // Implementation of GetActiveFlashSaleAsync method
        public async Task<SeasonalFlashSaleEntity> GetActiveFlashSaleAsync()
        {
            // Fetching the active flash sale from the repository or another source
            var flashSale = await _flashSaleRepository.GetActiveFlashSaleAsync();
            return flashSale;
        }
    }
}
