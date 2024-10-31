namespace DineConnect.OrderManagementService.Contracts.Order
{
    public record OrderResponse(
                 Guid Id,
                 Guid CustomerId,
                 Guid RestaurentId,
                 int OrderStatus, PaymentResponse PaymentResponse, List<OrderItemResponse> OrderItems)
    {
        public OrderResponse(Guid Id,
             Guid CustomerId,
             Guid RestaurentId,
             int OrderStatus,
             PaymentResponse PaymentResponse) : this(Id, CustomerId, RestaurentId, OrderStatus, PaymentResponse, new List<OrderItemResponse>()) { }

        // Parameterless constructor initializing with default values
        public OrderResponse()
            : this(
                Guid.Empty,
                Guid.Empty,
                Guid.Empty,
                0,
                PaymentResponse.Create(),
                new List<OrderItemResponse>())
        { }
    }

    public record OrderItemResponse(
        Guid Id,
        string Name,
        decimal Price, int Quantity);

    public record PaymentResponse(
        decimal Amount,
        int Status)
    {
        public static PaymentResponse Create()
        {
            return new PaymentResponse(0, 0);
        }
    }

}

