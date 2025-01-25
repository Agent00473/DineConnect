using DineConnect.RestaurantManagementService.Application.Ports;
using DineConnect.RestaurantManagementService.Domain.Catalogues;
using DineConnect.RestaurantManagementService.Domain.Catalogues.Entities;
using DineConnect.RestaurantManagementService.Domain.Catalogues.Ports;
using DineConnect.RestaurantManagementService.Domain.Catalogues.ValueObjects;
using DineConnect.RestaurantManagementService.Domain.Common;

namespace DineConnect.RestaurantManagementService.Application.Features.Restaurants
{
    internal class CatalogueQueryService: ICatalogueQueryService
    {
        #region Constants and Static Fields
        #endregion

        #region Private & Protected Fields
        private readonly ICatalogueRepository _catalogueRepository;
        #endregion

        #region Private & Protected Methods

        #endregion

        #region Constructors
        public CatalogueQueryService(ICatalogueRepository catalogueRepository)
        {
            _catalogueRepository = catalogueRepository;
        }

        #endregion

        #region Public Properties
        // Add public properties here
        #endregion

        #region Public Methods

        public bool CheckMenuItemAvailability(CatalogueId catalogueId, MenuItemId menuItemId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Catalogue>> GetCatalogAsync(int pageNumber, int pageSize)
        {
            return _catalogueRepository.GetPaginatedDataAsync(pageNumber, pageSize);
        }

        public Task<Catalogue> GetCatalogAsync(CatalogueId catalogueId)
        {
            return _catalogueRepository.GetByIdAsync(catalogueId);
        }

        public async Task<MenuItem> GetMenuItemAsync(CatalogueId catalogueId, MenuItemId menuItemId)
        {
            var catalogue = await GetCatalogAsync(catalogueId);
            catalogue. (menuItem);
            await _catalogueRepository.UpdateAsync(catalogue);
        }

        public Price GetMenuItemPrice(Guid catalogueId, MenuItemId menuItemId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MenuItem> GetMenuItems(Guid catalogueId, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MenuItem> GetMenuItemsByCategory(Guid catalogueId, ItemCategory category)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MenuItem> SearchMenuItems(CatalogueId catalogueId, string query, string category = null, decimal? minPrice = null, decimal? maxPrice = null)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
