using DineConnect.OrderManagementService.Domain.Common;
using DineConnect.OrderManagementService.Domain.Customers.ValueObjects;
using DineConnect.OrderManagementService.Domain.Orders.Entities;
using DineConnect.OrderManagementService.Domain.Orders.ValueObjects;
using DineConnect.OrderManagementService.Domain.Restaurant.ValueObjects;
using System.Collections.ObjectModel;

namespace DineConnect.OrderManagementService.Domain.Orders
{
    public class Order : AggregateRoot<OrderId, Guid>
    {
        private const string DefaultCurrency = "DKK";
        private IList<OrderItem> _orderItems;
        private void NotifyStatusChange(OrderStatus newStatus)
        {
            // Logic to notify status change, e.g., through an event
        }
        private Order(OrderId orderId, CustomerId customerid, RestaurantId restaurentId) : base(orderId)
        {
            CustomerId = customerid;
            RestaurantId = restaurentId;
            _orderItems = new List<OrderItem>();
            Status = OrderStatus.Pending;
        }
        private Order() { }
        public CustomerId CustomerId { get; private set; }
        public RestaurantId RestaurantId { get; private set; }
        public ReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
        public OrderStatus Status { get; private set; }
        public Payment Payment { get; private set; }
        public void AddItem(OrderItem item)
        {
            _orderItems.Add(item);
        }

        public void RemoveItem(OrderItem item)
        {
            _orderItems.Remove(item);
        }

        public void UpdateStatus(OrderStatus newStatus)
        {
            Status = newStatus;
            NotifyStatusChange(newStatus);
        }

        public void SetPayment(Payment payment)
        {
            Payment = payment;
        }

        public decimal CalculateTotal()
        {
            return _orderItems.Sum(item => item.Price * item.Quantity);
        }

        public static Order Create(OrderId orderId, CustomerId customerid, RestaurantId restaurentId)
        {
            return new Order(orderId, customerid, restaurentId);
        }

        public static Order Create(CustomerId customerid, RestaurantId restaurentId)
        {
            return new Order(OrderId.Create(), customerid, restaurentId);
        }

        public static Order Create(Guid customerid, Guid restaurentId)
        {
            return new Order(OrderId.Create(), CustomerId.Create(customerid), RestaurantId.Create(restaurentId));
        }
    }

}
