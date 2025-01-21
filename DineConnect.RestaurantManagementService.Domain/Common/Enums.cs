namespace DineConnect.RestaurantManagementService.Domain.Common
{
    public enum PaymentStatus
    {
        Unknown = 0,
        Pending = 1,  // Payment is initiated but not completed
        Completed,   // Payment has been successfully completed
        Failed,      // Payment attempt failed
        Refunded,    // Payment has been refunded
        Cancelled     // Payment has been cancelled by the user or system
    }

    public enum ItemCategory
    {
        None = 0,
        Starter = 1,
        Maincourse = 2,
        Dessert = 3,
        SoftDrinks = 4
    }
}
