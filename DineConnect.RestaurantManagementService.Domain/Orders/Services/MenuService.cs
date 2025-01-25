using DineConnect.RestaurantManagementService.Domain.Catalogues.Entities;


namespace DineConnect.RestaurantManagementService.Domain.Orders.Services
{
    internal class MenuService: IMenuService
    {
        #region Constants and Static Fields
        #endregion

        #region Private & Protected Fields
        #endregion

        #region Private & Protected Methods

        #endregion

        #region Constructors
        public MenuService(/* parameters */)
        {
            // Initialization code
        }
        #endregion

        #region Public Properties
        // Add public properties here
        #endregion

        #region Public Methods
        public List<MenuItem> GetAvailableMenuItems(Menu menu)
        {
            return menu.Items.Where(i => i.IsAvailable).ToList();
        }

        public List<MenuItem> FilterMenuItemsByCategory(Menu menu, Category category)
        {
            return menu.Items.Where(i => i.Category == category).ToList();
        }

        public List<MenuItem> GetSpecialMenuItems(Menu menu, SpecialOffer offer)
        {
            return menu.Items.Where(i => i.SpecialOffers.Contains(offer)).ToList();
        }
        #endregion

        #region Factory Methods
        // Factory Method
        public static MenuService Create(/* parameters */)
        {
            return new MenuService(/* arguments */);
        }
        #endregion
    }
}

}
