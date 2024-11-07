using MediatR;


namespace DineConnect.OrderManagementService.Application.Features.Orders.Command
{
    public record CreateOrderCommand(OrderCommandModel data) : IRequest<OrderResponseWrapper>
    {
    }

    public record OrderCommandModel(
             Guid CustomerId, Guid RestaurantId,
             int OrderStatus, PaymentCommandModel PaymentResponse, IEnumerable<OrderItemCommandModel> MenuItems)
    {
        public OrderCommandModel(
             Guid CustomerId, Guid RestaurantId, int OrderStatus,
             PaymentCommandModel PaymentResponse) : this(CustomerId, RestaurantId, OrderStatus, PaymentResponse, new List<OrderItemCommandModel>()) { }
        public OrderCommandModel(): this(Guid.Empty, Guid.Empty, 0, new PaymentCommandModel(0,0), new List<OrderItemCommandModel>()) { }
    }

    public record OrderItemCommandModel(string Name, decimal Price, int Quantity);

    public record PaymentCommandModel(decimal Amount, int PaymentMethod);
}
