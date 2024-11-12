using DineOutApp.Grpc;
using Grpc.Core;
using Infrastructure.GRPC.Protos;

namespace DineConnect.PromotionsManagementService.API.Services
{
    public class SeasonalFlashSaleServiceImplementation : SeasonalFlashSale.SeasonalFlashSaleBase
    {
        public override Task<SeasonalDiscountResponse> GetSeasonalDiscount(SeasonalDiscountRequest request, ServerCallContext context)
        {
            var response = new SeasonalDiscountResponse();

            // Business logic for seasonal discounts
            if (request.EventType == "flash_sale")
            {
                response.DiscountAvailable = true;
                response.DiscountPercentage = 20.0; // 20% discount for flash sales
                response.Message = "Flash sale discount applied!";
            }
            else if (request.EventType == "holiday")
            {
                response.DiscountAvailable = true;
                response.DiscountPercentage = 15.0; // 15% discount for holidays
                response.Message = "Holiday discount applied!";
            }
            else
            {
                response.DiscountAvailable = false;
                response.DiscountPercentage = 0.0;
                response.Message = "No discounts available for the selected event type.";
            }

            return Task.FromResult(response);
        }
    }
}

