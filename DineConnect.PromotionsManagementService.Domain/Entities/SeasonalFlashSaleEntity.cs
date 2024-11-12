
namespace DineConnect.PromotionsManagementService.Domain.Entities
{
    public class SeasonalFlashSaleEntity
    {
        public int Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid RestaurantId { get; set; }
        public double DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Business logic for checking if the sale is active
        public bool IsActive()
        {
            return DateTime.Now >= StartDate && DateTime.Now <= EndDate;
        }
    }
}
