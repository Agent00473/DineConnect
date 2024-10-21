
namespace DineConnect.OrderManagementService.Domain.Order.ValueObjects
{
    public enum OrderStatus
    {
        Pending = 0,     // Order has been created but not yet processed
        Confirmed,      // Order has been confirmed by the restaurant
        InPreparation,  // Order is currently being prepared
        ReadyForPickup, // Order is ready for delivery or pickup
        OutForDelivery, // Order is currently being delivered
        Completed,      // Order has been delivered successfully
        Cancelled,      // Order has been cancelled by the customer or restaurant
        Refunded        // Order payment has been refunded
    }

    public enum PaymentStatus
    {
        Pending = 0,  // Payment is initiated but not completed
        Completed,   // Payment has been successfully completed
        Failed,      // Payment attempt failed
        Refunded,    // Payment has been refunded
        Cancelled     // Payment has been cancelled by the user or system
    }

}
