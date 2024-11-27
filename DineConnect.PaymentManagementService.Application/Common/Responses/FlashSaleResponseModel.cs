
namespace DineConnect.PaymentManagementService.Application.Common.Responses
{
    public record FlashSaleResponseModel
    {
        public string SaleName { get; init; }
        public int SaleType { get; init; }
        public double DiscountPercentage { get; init; }
        public bool IsFreeDelivery { get; init; }
        public string ProductCategory { get; init; }
        public string BrandName { get; init; }

        // Private constructor
        private FlashSaleResponseModel(string saleName, int saleType, double discountPercentage, bool isFreeDelivery, string productCategory, string brandName)
        {
            SaleName = saleName;
            SaleType = saleType;
            DiscountPercentage = discountPercentage;
            IsFreeDelivery = isFreeDelivery;
            ProductCategory = productCategory;
            BrandName = brandName;
        }

        // Static Create method to instantiate the record
        public static FlashSaleResponseModel Create(string saleName, int saleType, double discountPercentage, bool isFreeDelivery, string productCategory, string brandName)
        {
            return new FlashSaleResponseModel(saleName, saleType, discountPercentage, isFreeDelivery, productCategory, brandName);
        }
    }

}
