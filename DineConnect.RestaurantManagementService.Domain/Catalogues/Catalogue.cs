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
        #endregion

        #region Public Methods

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
