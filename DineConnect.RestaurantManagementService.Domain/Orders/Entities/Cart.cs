using DineConnect.RestaurantManagementService.Domain.Catalogues.Entities;
using DineConnect.RestaurantManagementService.Domain.Common;
using DineConnect.RestaurantManagementService.Domain.Orders.ValueObjects;
using Infrastructure.Domain.Entities;

namespace DineConnect.RestaurantManagementService.Domain.Orders.Entities
{
    public class Cart : BaseEntity<CartId>
    {
        #region Constants and Static Fields
        #endregion

        #region Private & Protected Fields
        private List<OrderItem> _orderItems = new();

        #endregion

        #region Private & Protected Methods

        #endregion

        #region Constructors
        private Cart(CartId id, IList<OrderItem> items): this(id)
        {

        }
        private Cart(CartId id): base(id) 
        {

        }
        #endregion

        #region Public Properties
        public IReadOnlyList<OrderItem> Items => _orderItems.AsReadOnly();
        #endregion

        #region Public Methods
        public Price GetTotal()
        {
            return Price.Create(10);
        }

        // Add an item to the cart
        public void AddItem(MenuItem menuItem, int quantity)
        {
            var existingItem = _orderItems.FirstOrDefault(item => item.MenuItem.Id == menuItem.Id);

            if (existingItem != null)
            {
                // If the item exists, increase the quantity
                existingItem.IncreaseQuantity(quantity);
            }
            else
            {
                // If the item doesn't exist, add a new OrderItem
                var newItem = OrderItem.Create(menuItem, quantity);
                _orderItems.Add(newItem);
            }
        }

        // Remove an item from the cart
        public void RemoveItem(MenuItem menuItem, int quantity)
        {
            var existingItem = _orderItems.FirstOrDefault(item => item.MenuItem.Id == menuItem.Id);
            if (existingItem != null)
            {
                if (existingItem.Quantity > quantity)
                {
                    // Decrease quantity
                    existingItem.DecreaseQuantity(quantity);
                }
                else
                {
                    // Remove the item entirely
                    _orderItems.Remove(existingItem);
                }
            }
            else
            {
                throw new InvalidOperationException("Item not found in the cart.");
            }
        }

        public IReadOnlyList<OrderItem> GetItems()
        {
            return _orderItems.AsReadOnly();
        }

        public decimal GetTotalPrice()
        {
            return _orderItems.Sum(item => item.MenuItem.Price.Amount * item.Quantity);
        }

        #endregion


        #region Factory Methods
        public static Cart Create(CartId id, IList<MenuItem> items)
        {
            var cart = new Cart(id);
            foreach (var item in items)
            {
                cart.AddItem(item, 1);
            }
            return cart;
        }

        public static Cart Create()
        {
            return new Cart(CartId.Create());
        }
        #endregion
    }
}
