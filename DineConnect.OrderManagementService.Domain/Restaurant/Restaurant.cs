using DineConnect.OrderManagementService.Domain.Common;
using DineConnect.OrderManagementService.Domain.Order.ValueObjects;
using DineConnect.OrderManagementService.Domain.Restaurant.ValueObjects;

namespace DineConnect.OrderManagementService.Domain.Order
{
    public class Restaurant : AggregateRoot<RestaurantId, Guid>
    {
        private ICollection<OrderId> _orderIds;
        private void NotifyOrderUpdated()
        {
            // Logic to notify about menu updates, e.g., through an event or logging
        }
        private Restaurant(RestaurantId restaurantId, string name, IList<OrderId> orders) : base(restaurantId)
        {
            Name = name;
            _orderIds = orders;
        }

        private Restaurant(RestaurantId restaurantId, string name) : this(restaurantId, name, new List<OrderId>())
        {
        }

        private Restaurant()
        {
            
        }
        public string Name { get; private set; }
        public IReadOnlyList<OrderId> OrderIds => _orderIds.ToList().AsReadOnly();
        public void AddOrder(Order order)
        {
            _orderIds.Add(order.Id);
            NotifyOrderUpdated();
        }
        public static Restaurant Create(string name)
        {
            return new Restaurant(RestaurantId.Create(), name);
        }
        public static Restaurant Create(RestaurantId id, string name, IList<OrderId> orders)
        {
            return new Restaurant(id, name, orders);
        }
    }

}
