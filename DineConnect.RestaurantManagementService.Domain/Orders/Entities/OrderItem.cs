using DineConnect.RestaurantManagementService.Domain.Catalogues.Entities;
using Infrastructure.Domain.Entities;

namespace DineConnect.RestaurantManagementService.Domain.Orders.Entities
{
    /// <summary>
    /// Just a wrapper class not persisted 
    /// </summary>
    public class OrderItem : BaseEntity<Guid>
    {
        #region Constants and Static Fields
        #endregion

        #region Private & Protected Fields
        #endregion

        #region Private & Protected Methods

        #endregion

        #region Constructors
        private OrderItem(MenuItem menuItem, int quantity) : base(Guid.NewGuid())
        {
            MenuItem = menuItem ?? throw new ArgumentNullException(nameof(menuItem));
            if (quantity <= 0) throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));
            Quantity = quantity;
        }
        #endregion

        #region Public Properties
        public MenuItem MenuItem { get; private set; }
        public int Quantity { get; private set; }


        #endregion

        #region Public Methods
        public void IncreaseQuantity(int quantity)
        {
            if (quantity <= 0) throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));
            Quantity += quantity;
        }

        public void DecreaseQuantity(int quantity)
        {
            if (quantity <= 0) throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

            if (Quantity - quantity < 0)
                throw new InvalidOperationException("Cannot decrease quantity below zero.");

            Quantity -= quantity;
        }


        #endregion

        #region Factory Methods
        // Factory Method
        public static OrderItem Create(MenuItem menuItem, int quantity)
        {
            return new OrderItem(menuItem, quantity);
        }


        #endregion
    }
}
