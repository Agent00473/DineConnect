
using DineConnect.PaymentManagementService.Application.Common.Responses;

namespace DineConnect.PaymentManagementService.Application.Common.Interfaces
{

    public interface IPromotionService 
    {
        FlashSaleResponseModel GetFlashSaleDetails<TRequest>(TRequest request) ;
        DiscountResponseModel GetDiscountDetails<TRequest>(TRequest request);
    }
}
