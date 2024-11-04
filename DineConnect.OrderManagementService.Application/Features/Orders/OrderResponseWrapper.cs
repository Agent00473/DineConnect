using DineConnect.OrderManagementService.Application.Common;
using DineConnect.OrderManagementService.Application.Features.Orders.Query;
using DineConnect.OrderManagementService.Domain.Common;
using DineConnect.OrderManagementService.Domain.Orders;

namespace DineConnect.OrderManagementService.Application.Features.Orders
{
    public sealed class OrderResponseWrapper: ResponseWrapper<IEnumerable<OrderResponse>, OrderErrorCode>
    {
        private OrderResponseWrapper(IEnumerable<OrderResponse> value) : base(value)
        {
        }

        private OrderResponseWrapper(ErrorDetails<OrderErrorCode> error) : base(error)
        {
        }
        public static implicit operator OrderResponseWrapper(List<OrderResponse> value) => new OrderResponseWrapper(value);

        public static implicit operator OrderResponseWrapper(ErrorDetails<OrderErrorCode> error) => new OrderResponseWrapper(error);

    }
}
