using DineConnect.PromotionsManagementService.Domain.Common;

namespace DineConnect.PromotionsManagementService.Domain.FlashSales.ValueObjects
{
    public class DiscountTier : ValueObject
    {
        private DiscountTier() { }
        public double MinimumOrderValue { get; set; }
        public double DiscountAmount { get; set; }
        public bool IsFlatDiscount { get; set; }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return MinimumOrderValue; 
            yield return DiscountAmount;
            yield return IsFlatDiscount;
        }

        public static DiscountTier Create(double minOrderValue, double discountAmount, bool flatdiscount)
        {
            return new DiscountTier()
            {
                MinimumOrderValue = minOrderValue,
                DiscountAmount = discountAmount,
                IsFlatDiscount = flatdiscount
            };
        }
    }

}
