
namespace DineConnect.PromotionsManagementService.Domain.Entities
{
    public class DeliveryChargeWaiverEntity
    {
        public Guid CustomerId { get; set; }
        public bool IsEligible { get; set; }
        public double WaivedAmount { get; set; }
        public string Message { get; set; }
    }
}
