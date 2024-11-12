using DineConnect.PromotionsManagementService.Domain.FlashSales.Entities;
using DineConnect.PromotionsManagementService.Domain.FlashSales.ValueObjects;

namespace DineConnect.PromotionsManagementService.Application.Interfaces
{
    public interface IFlashSaleRepository
    {
        Task<FlashSale> GetFlashSaleByIdAsync(Guid flashSaleId);
        Task<FlashSale> GetFlashSaleByTypeAsync(FlashSaleType saleType);
        Task AddFlashSaleAsync(FlashSale flashSale);
        Task UpdateFlashSaleAsync(FlashSale flashSale);
        Task DeleteFlashSaleAsync(Guid flashSaleId);
        Task<Customer> GetCustomerAsync(Guid customerId);
    }
}
