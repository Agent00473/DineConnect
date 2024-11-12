using DineConnect.PromotionsManagementService.Domain.Entities;

namespace DineConnect.PromotionsManagementService.Application.Usecases
{
    public interface IDeliveryChargeWaiverService
    {
        Task<DeliveryChargeWaiverEntity> CheckDeliveryChargeWaiverAsync(Guid customerId, double orderAmount);
    }
}
