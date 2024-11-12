
using DineConnect.PromotionsManagementService.Domain.Entities;

namespace DineConnect.PromotionsManagementService.Application
{
    public interface IDeliveryChargeWaiverRepository
    {
        Task<DeliveryChargeWaiverEntity> CheckWaiverEligibilityAsync(Guid customerId, double orderAmount);
    }
}
