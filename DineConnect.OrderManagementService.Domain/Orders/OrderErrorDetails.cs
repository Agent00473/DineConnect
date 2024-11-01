using DineConnect.OrderManagementService.Domain.Common;

namespace DineConnect.OrderManagementService.Domain.Orders
{
    public enum OrderErrorCode
    {
        None = 0,
        InvalidOrderId = 1,
        OrderNotFound = 2,
        PaymentFailed = 3,
        OrderAlreadyCompleted = 4,
        OutOfStock = 5
    }

    public sealed record OrderErrorDetails
    {
        public static readonly ErrorDetails<OrderErrorCode> None = new(ErrorType.None, OrderErrorCode.None, string.Empty);
        public static readonly ErrorDetails<OrderErrorCode> InvalidOrderId = new(ErrorType.Validation, OrderErrorCode.InvalidOrderId, "Order ID is not valid.");
        public static readonly ErrorDetails<OrderErrorCode> OrderNotFound = new(ErrorType.NotFound, OrderErrorCode.OrderNotFound, "Order not found.");
        public static readonly ErrorDetails<OrderErrorCode> PaymentFailed = new(ErrorType.Conflict, OrderErrorCode.PaymentFailed, "Payment for the order has failed.");
        public static readonly ErrorDetails<OrderErrorCode> OrderAlreadyCompleted = new(ErrorType.Conflict, OrderErrorCode.OrderAlreadyCompleted, "Order has already been completed.");
        public static readonly ErrorDetails<OrderErrorCode> OutOfStock = new(ErrorType.Conflict, OrderErrorCode.OutOfStock, "Item is out of stock.");
    }
}
