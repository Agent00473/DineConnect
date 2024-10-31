namespace DineConnect.OrderManagementService.Contracts.Order
{
    public record NewOrderRequest(
             Guid CustomerId,
             Guid RestaurentId,
             int OrderStatus, NewPaymentRequest PaymentResponse, IEnumerable<NewOrderItemRequest> MenuItems)
    {
        public NewOrderRequest(
             Guid CustomerId,
             Guid RestaurentId,
             int OrderStatus,
             NewPaymentRequest PaymentResponse) : this(CustomerId, RestaurentId, OrderStatus, PaymentResponse, new List<NewOrderItemRequest>()) { }
    }

    public record NewOrderItemRequest(
        string Name,
        decimal Price, int Quantity);

    public record NewPaymentRequest(
        decimal Amount,
        int PaymentMethod);
}
