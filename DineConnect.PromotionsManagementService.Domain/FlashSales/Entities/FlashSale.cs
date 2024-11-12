using DineConnect.PromotionsManagementService.Domain.Common;
using DineConnect.PromotionsManagementService.Domain.FlashSales.ValueObjects;

namespace DineConnect.PromotionsManagementService.Domain.FlashSales.Entities
{
    public class FlashSale : BaseEntity<Guid>
    {
        private List<DiscountTier> _tiers = new();

        private FlashSale() : base()
        {
            Id = Guid.NewGuid();
        }

        private FlashSale(Guid id) : base(id)
        {
        }

        public string SaleName { get; set; }
        public FlashSaleType SaleType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IReadOnlyCollection<DiscountTier> DiscountTiers => _tiers.AsReadOnly();

        public string ProductCategory { get; set; }
        public string BrandName { get; set; }

        public void AddDiscountTier(DiscountTier tier)
        {
            _tiers.Add(tier);
        }

        public void RemoveDiscountTier(DiscountTier tier)
        {
            _tiers.Remove(tier);
        }

        public void ClearDiscountTier(DiscountTier tier)
        {
            _tiers.Clear();
        }
        public static FlashSale Create()
        {
            return new FlashSale();
        }

        public static FlashSale Create(Guid id)
        {
            return new FlashSale(id);
        }

        public static FlashSale Create(Guid id, string saleName, FlashSaleType type, DateTime startDate, DateTime endDate, string productCategory, 
            string brandName, IEnumerable<DiscountTier> tiers)
        {
            var obj = new FlashSale(id)
            {
                SaleName = saleName,
                SaleType = type,
                StartDate = startDate,
                EndDate = endDate,
                ProductCategory = productCategory,
                BrandName = brandName

            };
            foreach (var item in tiers)
            {
                obj.AddDiscountTier(item); 
            }
            return obj;
        }
    }

}
