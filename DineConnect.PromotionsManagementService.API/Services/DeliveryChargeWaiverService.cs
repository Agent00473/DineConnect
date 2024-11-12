using Grpc.Core;
using Infrastructure.GRPC.Protos;  

namespace DineConnect.PromotionsManagementService.API.Services
{
    public class DeliveryChargeWaiverService : DeliveryChargeWaiver.DeliveryChargeWaiverBase
    {
        //Implementing the GetDeliveryChargeWaiver RPC
        public override Task<DeliveryChargeWaiverResponse> GetDeliveryChargeWaiver(DeliveryChargeWaiverRequest request, ServerCallContext context)
        {
            var response = new DeliveryChargeWaiverResponse();

            // Example business logic: If the order amount is greater than or equal to $50, apply a waiver
            if (request.OrderAmount >= 50.0)
            {
                response.EligibleForWaiver = true;
                response.WaivedAmount = 5.0;  // Flat $5 waiver
                response.Message = "You are eligible for a delivery charge waiver!";
            }
            else
            {
                response.EligibleForWaiver = false;
                response.WaivedAmount = 0.0;
                response.Message = "No waiver available for this order amount.";
            }

            return Task.FromResult(response);
        }
    }
}
