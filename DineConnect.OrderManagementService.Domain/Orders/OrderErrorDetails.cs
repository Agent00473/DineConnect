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
        OutOfStock = 5,
        DuplicateOrder = 6,
        InvalidQuantity = 7,
        InvalidProduct = 8,
        MissingData = 9,
        InvalidPrice = 10,
        UnauthorizedAccess = 11
    }

    public sealed record OrderErrorDetails
    {
        public static readonly ErrorDetails<OrderErrorCode> None = new(OrderErrorCode.None, string.Empty);
        public static readonly ErrorDetails<OrderErrorCode> InvalidOrderId = new(OrderErrorCode.InvalidOrderId, "Order ID is not valid.");
        public static readonly ErrorDetails<OrderErrorCode> OrderNotFound = new(OrderErrorCode.OrderNotFound, "Order not found.");
        public static readonly ErrorDetails<OrderErrorCode> PaymentFailed = new(OrderErrorCode.PaymentFailed, "Payment for the order has failed.");
        public static readonly ErrorDetails<OrderErrorCode> OrderAlreadyCompleted = new(OrderErrorCode.OrderAlreadyCompleted, "Order has already been completed.");
        public static readonly ErrorDetails<OrderErrorCode> OutOfStock = new(OrderErrorCode.OutOfStock, "Item is out of stock.");
        public static readonly ErrorDetails<OrderErrorCode> DuplicateOrder = new(OrderErrorCode.DuplicateOrder, "Order already exists.");
        public static readonly ErrorDetails<OrderErrorCode> InvalidQuantity = new(OrderErrorCode.InvalidQuantity, "Order quantity is invalid.");
        public static readonly ErrorDetails<OrderErrorCode> InvalidProduct = new(OrderErrorCode.InvalidProduct, "Product is invalid.");
        public static readonly ErrorDetails<OrderErrorCode> NullData = new(OrderErrorCode.MissingData, "Data cannot be empty.");
        public static readonly ErrorDetails<OrderErrorCode> InvalidPrice = new(OrderErrorCode.InvalidPrice, "Order price is invalid.");
        public static readonly ErrorDetails<OrderErrorCode> UnauthorizedAccess = new(OrderErrorCode.UnauthorizedAccess, "Unauthorized access to order.");
    }
}
