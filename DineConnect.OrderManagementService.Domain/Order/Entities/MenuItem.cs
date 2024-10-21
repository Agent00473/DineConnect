using DineConnect.OrderManagementService.Domain.Common;
using DineConnect.OrderManagementService.Domain.Order.ValueObjects;

namespace DineConnect.OrderManagementService.Domain.Order.Entities
{

    public class MenuItem : BaseEntity<MenuItemId>
    {
        #region Constants and Static Fields
        #endregion

        #region Private Fields
        #endregion

        #region Protected Fields
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods
        #endregion

        #region Constructors
        private MenuItem(MenuItemId itemId, string itemName, decimal price, int quantity) : base(itemId)
        {
            ItemName = itemName;
            Price = price;
            Quantity = quantity;
        }
        #endregion

        #region Public Properties
        public string ItemName { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
        #endregion

        #region Public Methods
        #endregion

        #region Interface Implementations
        #endregion

        #region Factory Methods
        public static MenuItem Create(MenuItemId itemId, string itemName, decimal price, int quantity)
        {
            return new MenuItem(itemId, itemName, price, quantity);
        }
        #endregion
    }

}
