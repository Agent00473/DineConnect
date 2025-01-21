using DineConnect.RestaurantManagementService.Domain.Common;
using DineConnect.RestaurantManagementService.Domain.Orders.Entities;
using DineConnect.RestaurantManagementService.Domain.Orders.ValueObjects;
using DineConnect.RestaurantManagementService.Domain.Resturants;
using Infrastructure.Domain.Entities;

namespace DineConnect.RestaurantManagementService.Domain.Orders
{
    public class Order : BaseAggregateRoot<OrderId, Guid>
    {
        #region Constants and Static Fields
        #endregion

        #region Private & Protected Fields
        private readonly Customer _customer;
        private readonly Cart _cart;
        #endregion

        #region Private & Protected Methods

        #endregion

        #region Constructors
        private Order(OrderId id, string name, RestaurantId restaurantId, Customer customer) : base(id)
        {
            Name = name;
            _customer = customer;
            RestaurantId = restaurantId;
            PaymentId = null;
            _cart = Cart.Create();
        }

        private Order(OrderId id, string name, RestaurantId restaurantId, Customer customer, Cart cart) : base(id)
        {
            Name = name;
            _customer = customer;
            RestaurantId = restaurantId;
            PaymentId = null;
            _cart = cart;
        }
        #endregion

        #region Public Properties
        public string Name { get; private set; } = string.Empty;
        public PaymentId? PaymentId { get; private set; }
        public RestaurantId RestaurantId { get; private set; }

        public Price TotalPrice { get; private set; }
        public Customer Customer => _customer;

        public Cart Cart => _cart;
        #endregion

        #region Public Methods

        #endregion

        #region Factory Methods
        public static Order Create(OrderId id, string name, RestaurantId restaurantId, Customer customer)
        {
            return new Order(id, name, restaurantId, customer);
        }
        public static Order Create(OrderId id, string name, RestaurantId restaurantId, Customer customer, Cart cart)
        {
            return new Order(id, name, restaurantId, customer,cart);
        }
        #endregion

    }
}
