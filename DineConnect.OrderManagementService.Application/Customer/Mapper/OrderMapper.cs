
using DineConnect.OrderManagementService.Contracts.Order;
using DineConnect.OrderManagementService.Domain.Customers.ValueObjects;
using DineConnect.OrderManagementService.Domain.Orders;
using DineConnect.OrderManagementService.Domain.Orders.Entities;
using DineConnect.OrderManagementService.Domain.Orders.ValueObjects;
using DineConnect.OrderManagementService.Domain.Restaurant.ValueObjects;

namespace DineConnect.OrderManagementService.Application.Customer.Mapper
{
    public static class OrderMapper
    {
        public static Order CreateOrder(this NewOrderRequest request)
        {
            var order = Order.Create(CustomerId.Create(request.CustomerId), RestaurantId.Create(request.RestaurentId));
            request.MenuItems.CreateOrderItem(order);
            order.SetPayment(request.PaymentResponse.CreatePayment());
            return order;
        }

        private static Payment CreatePayment(this NewPaymentRequest request)
        {
            return Payment.Create(request.Amount, (PaymentMethod)request.PaymentMethod);
        }

        private static IEnumerable<OrderItem> CreateOrderItem(this IEnumerable<NewOrderItemRequest> request, Order order)
        {
            foreach (var item in request)
            {
                order.AddItem(OrderItem.Create(item.Name, item.Price));
            }
            return order.OrderItems;
        }

        public static OrderResponse CreateNewOrderResponse(this Order order)
        {
            var result = new OrderResponse(order.Id.IdValue, order.CustomerId.IdValue, order.RestaurantId.IdValue, (int)order.Status, order.Payment.CreatePaymentResponse());
            var orderItems = order.OrderItems.CreateOrderItemResponse(result);
            return result;
        }

        private static PaymentResponse CreatePaymentResponse(this Payment request)
        {
            return new PaymentResponse(request.Amount, (int)request.Status);
        }

        private static IEnumerable<OrderItemResponse> CreateOrderItemResponse(this IEnumerable<OrderItem> items, OrderResponse order)
        {
            foreach (var item in items)
            {
                order.OrderItems.Add(new OrderItemResponse(item.Id.IdValue, item.ItemName, item.Price, item.Quantity));
            }
            return order.OrderItems;
        }

    }
}
