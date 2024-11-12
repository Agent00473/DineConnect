using DineConnect.PromotionsManagementService.Domain.Common;

namespace DineConnect.PromotionsManagementService.Domain.FlashSales.ValueObjects
{
    public class DiscountCriteria : ValueObject
    {
        private DiscountCriteria(): base()
        {
            Location = string.Empty;
        }
        public double Percentage { get; private set; }
        public bool IsLocationBased { get; private set; }
        public bool IsVolumePricing { get; private set; }
        public string Location { get; private set; }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Percentage;
            yield return IsLocationBased;
            yield return IsVolumePricing;
            yield return Location;
        }

        public static DiscountCriteria Create()
        {
            return new DiscountCriteria(); 
        }

        public static DiscountCriteria Create(double percentage, bool isLocationBased, bool isVolumePricing, string location)
        {
            return new DiscountCriteria()
            {
                Location = location,
                Percentage = percentage,
                IsLocationBased = isLocationBased,
                IsVolumePricing = isVolumePricing
            };
        }


    }

}
