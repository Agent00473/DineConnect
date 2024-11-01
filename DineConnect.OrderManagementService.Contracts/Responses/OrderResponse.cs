using DineConnect.OrderManagementService.Application.Interfaces.Responses;

namespace DineConnect.OrderManagementService.Contracts.Responses
{
   public record OrderResponse(
                     Guid Id,
                     Guid CustomerId,
                     Guid RestaurentId,
                     int OrderStatus, IPaymentResponse PaymentResponse, List<IOrderItemResponse> OrderItems) : IOrderResponse
    {
        public OrderResponse(Guid Id,
             Guid CustomerId,
             Guid RestaurentId,
             int OrderStatus,
             IPaymentResponse PaymentResponse) : this(Id, CustomerId, RestaurentId, OrderStatus, PaymentResponse, new List<IOrderItemResponse>()) { }

        // Parameterless constructor initializing with default values
        public OrderResponse()
            : this(
                Guid.Empty,
                Guid.Empty,
                Guid.Empty,
                0,
                new PaymentResponse(0, 0),
                new List<IOrderItemResponse>())
        { }
    }

    public record OrderItemResponse(
        Guid Id,
        string Name,
        decimal Price, int Quantity) : IOrderItemResponse;



    public record PaymentResponse(
        decimal Amount,
        int Status) : IPaymentResponse
    {
        public static IPaymentResponse Create()
        {
            return new PaymentResponse(0, 0);
        }
    }

}

