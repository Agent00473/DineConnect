using DineConnect.OrderManagementService.Domain.Common;
using DineConnect.OrderManagementService.Domain.Order.ValueObjects;
using DineConnect.OrderManagementService.Domain.Restaurant.ValueObjects;

namespace DineConnect.OrderManagementService.Domain.Order
{
    internal class Restaurant : AggregateRoot<RestaurentId, Guid>
    {
        #region Constants and Static Fields
        #endregion

        #region Private Fields
        private IList<OrderId> _orders;
        #endregion

        #region Protected Fields
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods
        private void NotifyOrderUpdated()
        {
            // Logic to notify about menu updates, e.g., through an event or logging
        }
        #endregion

        #region Constructors
        private Restaurant(RestaurentId restaurantId, string name, IList<OrderId> orders) : base(restaurantId)
        {
            Name = name;
            _orders = orders;
        }

        private Restaurant(RestaurentId restaurantId, string name) : this(restaurantId, name, new List<OrderId>())
        {
        }

        #endregion

        #region Public Properties
        public Guid RestaurantId { get; private set; }
        public string Name { get; private set; }
        public IReadOnlyList<OrderId> Orders => _orders.AsReadOnly();
        #endregion

        #region Public Methods
        public void AddOrder(Order order)
        {
            _orders.Add(order.Id);
            NotifyOrderUpdated();
        }

        #endregion

        #region Interface Implementations
        #endregion

        #region Factory Methods
        public static Restaurant Create(string name)
        {
            return new Restaurant(RestaurentId.Create(), name);
        }
        public static Restaurant Create(RestaurentId id, string name, IList<OrderId> orders)
        {
            return new Restaurant(id, name, orders);
        }
        #endregion
    }

}
