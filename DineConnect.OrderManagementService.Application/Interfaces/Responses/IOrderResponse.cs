
namespace DineConnect.OrderManagementService.Application.Interfaces.Responses
{
    public interface IOrderResponse
    {
        Guid CustomerId { get; init; }
        Guid Id { get; init; }
        List<IOrderItemResponse> OrderItems { get; init; }
        int OrderStatus { get; init; }
        IPaymentResponse PaymentResponse { get; init; }
        Guid RestaurentId { get; init; }
    }

    public interface IOrderItemResponse
    {
        Guid Id { get; init; }
        string Name { get; init; }
        decimal Price { get; init; }
        int Quantity { get; init; }
    }

    public interface IPaymentResponse
    {
        decimal Amount { get; init; }
        int Status { get; init; }
    }

}
