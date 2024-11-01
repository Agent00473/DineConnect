using DineConnect.OrderManagementService.Application.Interfaces.Requests;

namespace DineConnect.OrderManagementService.Contracts.Requests
{
    
    public record NewOrderRequest(
                 Guid CustomerId, Guid RestaurentId,
                 int OrderStatus, INewPaymentRequest PaymentResponse, IEnumerable<INewOrderItemRequest> MenuItems) : INewOrderRequest
    {
        public NewOrderRequest(
             Guid CustomerId, Guid RestaurentId, int OrderStatus,
             INewPaymentRequest PaymentResponse) : this(CustomerId, RestaurentId, OrderStatus, PaymentResponse, new List<INewOrderItemRequest>()) { }
    }

    public record NewOrderItemRequest(
        string Name,  decimal Price, int Quantity): INewOrderItemRequest;

    public record NewPaymentRequest(
        decimal Amount,  int PaymentMethod) : INewPaymentRequest;
}
