
namespace DineConnect.PaymentManagementService.Application.Common.Responses
{
    public record DiscountResponseModel
    {
        public decimal DiscountAmount { get; init; }
        public decimal FinalPrice { get; init; }
        public string Message { get; init; }

        // Private constructor
        private DiscountResponseModel(decimal discountAmount, decimal finalPrice, string message)
        {
            DiscountAmount = discountAmount;
            FinalPrice = finalPrice;
            Message = message;
        }

        // Static Create method to instantiate the record
        public static DiscountResponseModel Create(decimal discountAmount, decimal finalPrice, string message)
        {
            return new DiscountResponseModel(discountAmount, finalPrice, message);
        }
    }

}
