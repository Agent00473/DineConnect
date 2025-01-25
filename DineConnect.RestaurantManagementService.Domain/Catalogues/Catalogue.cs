using DineConnect.RestaurantManagementService.Domain.Catalogues.Entities;
using DineConnect.RestaurantManagementService.Domain.Catalogues.ValueObjects;
using Infrastructure.Domain.Entities;


namespace DineConnect.RestaurantManagementService.Domain.Catalogues
{
    public class Catalogue : BaseAggregateRoot<CatalogueId, Guid>
    {
        #region Constants and Static Fields
        #endregion

        #region Private & Protected Fields
        private List<MenuItem> _menuItems = new();
        #endregion

        #region Private & Protected Methods

        #endregion

        #region Constructors
        public Catalogue(CatalogueId id) : base(id)
        {

        }
        public Catalogue(CatalogueId id, IEnumerable<MenuItem> menuItems) : base(id)
        {
            _menuItems.AddRange(menuItems);
        }

        #endregion

        #region Public Properties
        public IReadOnlyCollection<MenuItem> MenuItems => _menuItems.AsReadOnly();
        public string Name {  get; private set; }
        #endregion

        #region Public Methods
        public void UpdateDetails(string name, string description)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public void AddMenuItem(MenuItem menuItem)
        {
            if (_menuItems.Any(item => item.Id == menuItem.Id))
                throw new InvalidOperationException($"MenuItem with ID {menuItem.Id} already exists.");

            _menuItems.Add(menuItem);
        }

        public void UpdateMenuItem(MenuItemId menuItemId, MenuItem updatedMenuItem)
        {
            var menuItem = _menuItems.FirstOrDefault(item => item.Id == menuItemId);
            if (menuItem == null)
                throw new InvalidOperationException($"MenuItem with ID {menuItemId} not found.");

            menuItem.UpdateDetails(updatedMenuItem.Name, updatedMenuItem.Price);
        }

        public void RemoveMenuItem(MenuItemId menuItemId)
        {
            var menuItem = _menuItems.FirstOrDefault(item => item.Id == menuItemId);
            if (menuItem == null)
                throw new InvalidOperationException($"MenuItem with ID {menuItemId} not found.");

            _menuItems.Remove(menuItem);
        }

        public void GetMenuItem(MenuItemId menuItemId)
        {
            return _menuItems.FirstOrDefault(item => item.Id == menuItemId);
            return 
        }

        public void DisableMenuItem(MenuItemId menuItemId)
        {
            var menuItem = _menuItems.FirstOrDefault(item => item.Id == menuItemId);
            if (menuItem == null)
                throw new InvalidOperationException($"MenuItem with ID {menuItemId} not found.");

            menuItem.Disable();
        }

        public void EnableMenuItem(MenuItemId menuItemId)
        {
            var menuItem = _menuItems.FirstOrDefault(item => item.Id == menuItemId);
            if (menuItem == null)
                throw new InvalidOperationException($"MenuItem with ID {menuItemId} not found.");

            menuItem.Enable();
        }
        #endregion

        #region Factory Methods
        public static Catalogue Create(CatalogueId id, IEnumerable<MenuItem> menuItems)
        {
            return new Catalogue(id, menuItems);
        }
        public static Catalogue Create(CatalogueId id)
        {
            return new Catalogue(id);
        }
        public static Catalogue Create(IEnumerable<MenuItem> menuItems)
        {
            return new Catalogue(CatalogueId.Create(), menuItems);
        }
        #endregion
    }
}
