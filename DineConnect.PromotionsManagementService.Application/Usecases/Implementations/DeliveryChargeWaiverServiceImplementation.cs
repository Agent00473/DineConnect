using DineConnect.PromotionsManagementService.Domain.Entities;

namespace DineConnect.PromotionsManagementService.Application.Usecases.Implementations
{
    public class DeliveryChargeWaiverServiceImplementation : IDeliveryChargeWaiverService
    {
        private readonly IDeliveryChargeWaiverRepository _deliveryChargeWaiverRepository;

        public DeliveryChargeWaiverServiceImplementation(IDeliveryChargeWaiverRepository deliveryChargeWaiverRepository)
        {
            _deliveryChargeWaiverRepository = deliveryChargeWaiverRepository;
        }

        // Method implementation to check for delivery charge waiver eligibility
        public async Task<DeliveryChargeWaiverEntity> CheckDeliveryChargeWaiverAsync(Guid customerId, double orderAmount)
        {
            // Example: Call repository or business logic to determine waiver eligibility
            var waiver = await _deliveryChargeWaiverRepository.CheckWaiverEligibilityAsync(customerId, orderAmount);

            return new DeliveryChargeWaiverEntity
            {
                CustomerId = customerId,
                IsEligible = waiver.IsEligible,
                WaivedAmount = waiver.WaivedAmount,
                Message = waiver.Message
            };
        }
    }
}
