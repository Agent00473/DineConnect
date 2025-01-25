using DineConnect.RestaurantManagementService.Domain.Catalogues.Entities;
using DineConnect.RestaurantManagementService.Domain.Catalogues.ValueObjects;
using DineConnect.RestaurantManagementService.Domain.Common;
namespace DineConnect.RestaurantManagementService.Domain.Catalogues.Ports
{
    public interface ICatalogueCommandService
    {
        Task CreateCatalogueAsync(Catalogue catalogue);
        Task UpdateCatalogueAsync(CatalogueId catalogueId, string name, string description);
        Task UpdateCatalogDetailsAsync(Catalogue catalog);

        Task AddMenuItemAsync(CatalogueId catalogueId, MenuItem menuItem);
        Task UpdateMenuItemAsync(CatalogueId catalogueId, MenuItemId menuItemId, MenuItem updatedMenuItem);
        Task RemoveMenuItemAsync(CatalogueId catalogueId, MenuItemId menuItemId);

        Task EnableMenuItemAsync(CatalogueId catalogueId, MenuItemId menuItemId);
        Task DisableMenuItemAsync(CatalogueId catalogueId, MenuItemId menuItemId);
    }
}
