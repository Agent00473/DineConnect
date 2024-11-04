using MediatR;


namespace DineConnect.OrderManagementService.Application.Features.Orders.Command
{
    public record CreateOrderCommand(OrderCommandModel data) : IRequest<OrderResponseWrapper>
    {
    }

    public record OrderCommandModel(
             Guid CustomerId, Guid RestaurentId,
             int OrderStatus, PaymentCommandModel PaymentResponse, IEnumerable<OrderItemCommandModel> MenuItems)
    {
        public OrderCommandModel(
             Guid CustomerId, Guid RestaurentId, int OrderStatus,
             PaymentCommandModel PaymentResponse) : this(CustomerId, RestaurentId, OrderStatus, PaymentResponse, new List<OrderItemCommandModel>()) { }
    }

    public record OrderItemCommandModel(string Name, decimal Price, int Quantity);

    public record PaymentCommandModel(decimal Amount, int PaymentMethod);
}
