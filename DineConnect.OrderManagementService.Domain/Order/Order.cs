using DineConnect.OrderManagementService.Domain.Common;
using DineConnect.OrderManagementService.Domain.Customer.ValueObjects;
using DineConnect.OrderManagementService.Domain.Order.Entities;
using DineConnect.OrderManagementService.Domain.Order.ValueObjects;
using DineConnect.OrderManagementService.Domain.Restaurant.ValueObjects;
using System.Collections.ObjectModel;

namespace DineConnect.OrderManagementService.Domain.Order
{
    internal class Order : AggregateRoot<OrderId, Guid>
    {
        #region Constants and Static Fields
        private const string DefaultCurrency = "DKK";
        #endregion

        #region Private Fields
        private IList<MenuItem> _menuItems;
        #endregion

        #region Protected Fields
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods
        private void NotifyStatusChange(OrderStatus newStatus)
        {
            // Logic to notify status change, e.g., through an event
        }
        #endregion

        #region Constructors
        public Order(OrderId orderId, CustomerId customerid, RestaurentId restaurentId) : base(orderId)
        {
            CustomerId = customerid;
            RestaurentId = restaurentId;
            _menuItems = new List<MenuItem>();
            Status = OrderStatus.Pending;
            CreatedAt = DateTime.UtcNow;
        }
        #endregion

        #region Public Properties
        public CustomerId CustomerId { get; private set; }
        public RestaurentId RestaurentId { get; private set; }
        public ReadOnlyCollection<MenuItem> MenuItems => _menuItems.AsReadOnly();
        public OrderStatus Status { get; private set; }
        public Payment Payment { get; private set; }
        public DateTime CreatedAt { get; private set; }
        #endregion

        #region Public Methods
        public void AddItem(MenuItem item)
        {
            _menuItems.Add(item);
        }

        public void RemoveItem(MenuItem item)
        {
            _menuItems.Remove(item);
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
            return _menuItems.Sum(item => item.Price * item.Quantity);
        }
        #endregion

        #region Interface Implementations
        #endregion

        #region Factory Methods
        public static Order Create(OrderId orderId, CustomerId customerid, RestaurentId restaurentId)
        {
            return new Order(orderId, customerid, restaurentId);
        }

        public static Order Create(CustomerId customerid, RestaurentId restaurentId)
        {
            return new Order(OrderId.Create(), customerid, restaurentId);
        }
        #endregion
    }

}
