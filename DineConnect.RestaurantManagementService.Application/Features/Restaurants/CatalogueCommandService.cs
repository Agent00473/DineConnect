using DineConnect.RestaurantManagementService.Domain.Catalogues.Entities;
using DineConnect.RestaurantManagementService.Domain.Catalogues.Ports;
using DineConnect.RestaurantManagementService.Domain.Catalogues.ValueObjects;
using DineConnect.RestaurantManagementService.Application.Ports;
using DineConnect.RestaurantManagementService.Domain.Catalogues;

namespace DineConnect.RestaurantManagementService.Application.Features.Restaurants
{
    public class CatalogueCommandService : ICatalogueCommandService
    {
        #region Constants and Static Fields
        #endregion

        #region Private & Protected Fields
        private readonly ICatalogueRepository _catalogueRepository;
        #endregion

        #region Private & Protected Methods

        #endregion

        #region Constructors
        public CatalogueCommandService(ICatalogueRepository catalogueRepository)
        {
            _catalogueRepository = catalogueRepository;
        }
        #endregion

        #region Public Properties
        // Add public properties here
        #endregion

        #region Public Methods
        public Task<Catalogue?> GetCatalogueAsync(CatalogueId catalogueId)
        {
            return _catalogueRepository.GetByIdAsync(catalogueId)
                ?? throw new ArgumentException($"Catalogue with ID {catalogueId} not found.");
        }

        public Task CreateCatalogueAsync(Catalogue catalogue)
        {
            return _catalogueRepository.AddAsync(catalogue);
        }

        public async Task UpdateCatalogueAsync(CatalogueId catalogueId, string name, string description)
        {
            var catalogue = await GetCatalogueAsync(catalogueId);
            catalogue.UpdateDetails(name, description);
            await _catalogueRepository.UpdateAsync(catalogue);
        }

        public Task UpdateCatalogDetailsAsync(Catalogue catalog)
        {
            return _catalogueRepository.UpdateAsync(catalog);
        }

        public async Task AddMenuItemAsync(CatalogueId catalogueId, MenuItem menuItem)
        {
            var catalogue = await GetCatalogueAsync(catalogueId);
            catalogue.AddMenuItem(menuItem);
            await _catalogueRepository.UpdateAsync(catalogue);
        }

        public async Task UpdateMenuItemAsync(CatalogueId catalogueId, MenuItemId menuItemId, MenuItem updatedMenuItem)
        {
            var catalogue = await GetCatalogueAsync(catalogueId);
            catalogue.UpdateMenuItem(menuItemId, updatedMenuItem);
            await _catalogueRepository.UpdateAsync(catalogue);
        }

        public async Task RemoveMenuItemAsync(CatalogueId catalogueId, MenuItemId menuItemId)
        {
            var catalogue = await GetCatalogueAsync(catalogueId);
            catalogue.RemoveMenuItem(menuItemId);
            await _catalogueRepository.UpdateAsync(catalogue);
        }

        public async Task DisableMenuItemAsync(CatalogueId catalogueId, MenuItemId menuItemId)
        {
            var catalogue = await GetCatalogueAsync(catalogueId);
            catalogue.DisableMenuItem(menuItemId);
            await _catalogueRepository.UpdateAsync(catalogue);
        }

        public async Task EnableMenuItemAsync(CatalogueId catalogueId, MenuItemId menuItemId)
        {
            var catalogue = await GetCatalogueAsync(catalogueId);
            catalogue.EnableMenuItem(menuItemId);
            await _catalogueRepository.UpdateAsync(catalogue);
        }
        #endregion

    }

}
