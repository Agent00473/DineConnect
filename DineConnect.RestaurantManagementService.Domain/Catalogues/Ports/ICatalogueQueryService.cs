using DineConnect.RestaurantManagementService.Domain.Catalogues.Entities;
using DineConnect.RestaurantManagementService.Domain.Catalogues.ValueObjects;
using DineConnect.RestaurantManagementService.Domain.Common;
namespace DineConnect.RestaurantManagementService.Domain.Catalogues.Ports
{
    public interface ICatalogueQueryService
    {
        Task<IEnumerable<Catalogue>> GetCatalogAsync(int pageNumber, int pageSize);
        Task<Catalogue> GetCatalogAsync(CatalogueId catalogueId);
        IEnumerable<MenuItem> SearchMenuItems(CatalogueId catalogueId, string query, string category = null, decimal? minPrice = null, decimal? maxPrice = null);
        IEnumerable<MenuItem> GetMenuItemsByCategory(CatalogueId catalogueId, ItemCategory category);
        bool CheckMenuItemAvailability(CatalogueId catalogueId, MenuItemId menuItemId);
        Price GetMenuItemPrice(CatalogueId catalogueId, MenuItemId menuItemId);
        Task<MenuItem> GetMenuItemAsync(CatalogueId catalogueId, MenuItemId menuItemId);
        IEnumerable<MenuItem> GetMenuItems(CatalogueId catalogueId, int pageNumber, int pageSize);

    }
}
