using DineConnect.OrderManagementService.Application.Features.Customers.Query;
using DineConnect.OrderManagementService.Application.Features.Orders.Command;
using DineConnect.OrderManagementService.Application.Features.Orders.Query;
using DineConnect.OrderManagementService.Application.Interfaces.MapperFactories;
using DineConnect.OrderManagementService.Domain.Customers;
using DineConnect.OrderManagementService.Domain.Customers.ValueObjects;
using DineConnect.OrderManagementService.Domain.Orders;
using DineConnect.OrderManagementService.Domain.Orders.Entities;
using DineConnect.OrderManagementService.Domain.Orders.ValueObjects;
using DineConnect.OrderManagementService.Domain.Restaurant.ValueObjects;

namespace DineConnect.OrderManagementService.Contracts.Mapper
{
    /// <summary>
    /// Order DTO (Model) to Entity and vice versa
    /// </summary>
    public sealed class OrderModelEntityFactory : IRequestEntityFactory<OrderCommandModel, Order>
    {
        public IEnumerable<Order> CreateEntities(IEnumerable<OrderCommandModel> requests)
        {
            var result = new List<Order>();
            foreach (var item in requests)
            {
                result.Add(item.CreateOrder());
            }
            return result;
        }

        public Order CreateEntity(OrderCommandModel request)
        {
            return request.CreateOrder();
        }
    }

    /// <summary>
    /// Order Entity to Response (DTO) and vice versa
    /// </summary>
    public sealed class OrderResponseEntityFactory : IEntityResponseFactory<Order, OrderResponse>
    {
        public IEnumerable<OrderResponse> CreateResponses(IEnumerable<Order> entities)
        {
            var result = new List<OrderResponse>();
            foreach (var entity in entities)
            {
                result.Add(entity.CreateResponse());
            }
            return result;
        }

        public OrderResponse CreateResponse(Order entity)
        {
            return entity.CreateResponse();
        }
    }

    public static class OrderMapperExtensions
    {
        public static OrderResponse CreateResponse(this Order order)
        {
            return new OrderResponse( order.Id.IdValue, order.CustomerId.IdValue,
                 order.RestaurantId.IdValue, (int)order.Status,
                 order.OrderItems.Select(item => item.CreateResponse()).ToList(),
                 order.Payment.CreateResponse()
             );
        }

        public static IEnumerable<CustomerResponse> CreateResponses(this IEnumerable<Customer> customers)
        {
            var result = new List<CustomerResponse>();
            foreach (var customer in customers)
            {
                result.Add(customer.CreateResponse());
            }
            return result;
        }


        public static Order CreateOrder(this OrderCommandModel request)
        {
            var result = Order.Create(
                CustomerId.Create(request.CustomerId), RestaurantId.Create(request.RestaurantId));

            foreach (var item in request.MenuItems)
            {
                result.AddItem(item.CreateOrderItem());
            }
            result.SetPayment(request.PaymentResponse.CreatePayment());
            return result;
        }

        private static OrderItem CreateOrderItem(this OrderItemCommandModel model)
        {
            return OrderItem.Create(model.Name, model.Price, model.Quantity );
        }

        private static Payment CreatePayment(this PaymentCommandModel model)
        {
            return Payment.Create( model.Amount, (PaymentMethod)model.PaymentMethod // Map payment method to domain enum if needed
            );
        }

        private static OrderItemResponse CreateResponse(this OrderItem item)
        {
            return new OrderItemResponse(
                item.Id.IdValue,
                item.ItemName,
                item.Price,
                item.Quantity
            );
        }

        private static PaymentResponse CreateResponse(this Payment payment)
        {
            return new PaymentResponse(
                payment.Amount,
                (int)payment.Status
            );
        }
    }
}
