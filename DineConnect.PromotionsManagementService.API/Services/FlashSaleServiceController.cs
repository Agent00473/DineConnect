using DineConnect.PromotionsManagementService.Application.Usecases.FlashSales;
using DineConnect.PromotionsManagementService.Domain.FlashSales.ValueObjects;
using Grpc.Core;
using Infrastructure.GRPC.Protos;
using static Infrastructure.GRPC.Protos.FlashSaleService;

namespace DineConnect.PromotionsManagementService.API.Services
{
    public class FlashSaleServiceController: FlashSaleServiceBase
    {
        private readonly IFlashSaleManager _flashSaleManager;

        public FlashSaleServiceController(IFlashSaleManager flashSaleManager)
        {
            _flashSaleManager = flashSaleManager;
        }

        // Get flash sale details based on the type of flash sale requested
        public override async Task<FlashSaleResponse> GetFlashSaleDetails(FlashSaleRequest request, ServerCallContext context)
        {
            var flashSale = await _flashSaleManager.GetFlashSaleByTypeAsync((FlashSaleType) request.SaleType);

            if(flashSale == null)
            {
                return new FlashSaleResponse();
            }

            return new FlashSaleResponse
            {
                SaleName = flashSale.SaleName,
                SaleType = (int)flashSale.SaleType,
                DiscountPercentage = flashSale.DiscountTiers.ElementAt(0).DiscountAmount,
                IsFreeDelivery = flashSale.DiscountTiers.ElementAt(0).IsFlatDiscount,
                ProductCategory = flashSale.ProductCategory ?? string.Empty,
                BrandName = flashSale.BrandName ?? string.Empty
            };
        }

        // Calculate discount based on customer information and order amount
        public override async Task<DiscountResponse> CalculateDiscount(DiscountRequest request, ServerCallContext context)
        {
            var discount = await _flashSaleManager.CalculateDiscountAmount(Guid.Parse(request.CustomerId), request.OrderAmount, (FlashSaleType)request.SaleType);

            return new DiscountResponse
            {
                DiscountAmount = discount.Discount,
                FinalPrice = request.OrderAmount - discount.Discount,
                Message = discount.message
            };
        }
    }
}
