namespace DineConnect.PromotionsManagementService.Domain.FlashSales.ValueObjects
{
    public enum CustomerType
    {
        Premium = 0,
        Regular
    }

    public enum FlashSaleType
    {
        UnKnown = 0,
        Tiered,
        Holiday,
        Mystery,
        ProductCategorySpecific,
        SingleBrand,
        SpecialOccasion
    }

    public enum ProductCategory
    {
        Electronics,
        HomeAndKitchen,
        FashionAndApparel,
        HealthAndBeauty,
        SportsAndOutdoors,
        ToysAndGames
    }

}
