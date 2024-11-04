
namespace DineConnect.OrderManagementService.Application.Features.Orders.Query
{
    public sealed record OrderResponse(
                Guid Id, Guid CustomerId,  Guid RestaurentId, int OrderStatus,  List<OrderItemResponse> OrderItems, 
                PaymentResponse PaymentResponse)
    {
        public OrderResponse():this(Guid.Empty, Guid.Empty, Guid.Empty,0, Enumerable.Empty<OrderItemResponse>().ToList(), new PaymentResponse(0,0))
        {
            
        }
    }

    public sealed record OrderItemResponse(
                Guid Id,  string Name,  decimal Price,  int Quantity);

    public sealed record PaymentResponse(decimal Amount, int Status);

}
