
namespace DineConnect.OrderManagementService.Application.Interfaces.Requests
{
    public interface INewOrderRequest
    {
        Guid CustomerId { get; init; }
        IEnumerable<INewOrderItemRequest> MenuItems { get; init; }
        int OrderStatus { get; init; }
        INewPaymentRequest PaymentResponse { get; init; }
        Guid RestaurentId { get; init; }
    }

    public interface INewOrderItemRequest
    {
        string Name { get; init; }
        decimal Price { get; init; }
        int Quantity { get; init; }
    }

    public interface INewPaymentRequest
    {
        decimal Amount { get; init; }
        int PaymentMethod { get; init; }
    }
}
