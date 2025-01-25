using DineConnect.RestaurantManagementService.Domain.Catalogues.Entities;
using DineConnect.RestaurantManagementService.Domain.Orders.Entities;

namespace DineConnect.RestaurantManagementService.Domain.Orders.Services
{
    internal class CartService : ICartService
    {
        #region Constants and Static Fields
        #endregion

        #region Private & Protected Fields
        #endregion

        #region Private & Protected Methods

        #endregion

        #region Constructors
        public CartService(/* parameters */)
        {
            // Initialization code
        }
        #endregion

        #region Public Properties
        // Add public properties here
        #endregion

        #region Public Methods
        public bool AddItemToCart(Cart cart, MenuItem menuItem, int quantity)
        {
            var existingItem = cart.Items.FirstOrDefault(i => i.MenuItem.Id == menuItem.Id);
            if (existingItem != null)
            {
                existingItem.IncreaseQuantity(quantity);
            }
            else
            {
                cart.AddItem(menuItem, quantity);
            }
            return true;
        }

        public bool RemoveItemFromCart(Cart cart, MenuItem menuItem)
        {
             return cart.RemoveItem(menuItem, 1);
        }

        public decimal CalculateCartTotal(Cart cart)
        {
            return cart.GetTotalPrice();
        }

        //public void ApplyDiscount(Cart cart, Discount discount)
        //{
        //    var total = CalculateCartTotal(cart);
        //    if (discount.IsValidForCart(total))
        //    {
        //        cart.Discount = discount;
        //    }
        //}
        #endregion

        #region Factory Methods
        #endregion
    }


}
