using DineConnect.PromotionsManagementService.Domain.FlashSales.Entities;
using DineConnect.PromotionsManagementService.Domain.FlashSales.ValueObjects;

namespace DineConnect.PromotionsManagementService.Application.Usecases.FlashSales
{
    public record  DiscountData(double Discount, string message);

    public interface IFlashSaleManager
    {
        Task<DiscountData> CalculateDiscountAmount(Guid customerId, double orderAmount, FlashSaleType saleType);
        Task<FlashSale> GetFlashSaleByTypeAsync(FlashSaleType saleType);

    }
}