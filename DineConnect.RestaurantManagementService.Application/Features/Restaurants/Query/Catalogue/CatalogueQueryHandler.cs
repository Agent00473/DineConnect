using DineConnect.RestaurantManagementService.Application.Ports;
using DineConnect.RestaurantManagementService.Domain.Resturants;
using MediatR;

namespace DineConnect.RestaurantManagementService.Application.Features.Restaurants.Query.Catalogue
{
    public class CatalogueQueryHandler : IRequestHandler<CatalogueQuery, RestaurantAPIResponse<CatalogueResponse>>
    {
        #region Constants and Static Fields
        #endregion

        #region Private & Protected Fields
        private readonly ICatalogueRepository _catalogueRepository;
        #endregion

        #region Private & Protected Methods

        #endregion

        #region Constructors
        public CatalogueQueryHandler(ICatalogueRepository catalogueRepository)
        {
            _catalogueRepository = catalogueRepository ?? throw new ArgumentNullException(nameof(catalogueRepository));
        }
        #endregion

        #region Public Properties
        #endregion

        #region Public Methods
        public async Task<RestaurantAPIResponse<CatalogueResponse>> Handle(CatalogueQuery request, CancellationToken cancellationToken)
        {
            // Validate the request
            if (request.RestaurantId == Guid.Empty)
            {
                throw new ArgumentException("Invalid Restaurant ID.", nameof(request.RestaurantId));
            }

            // Fetch catalogue from the repository
            var menuItems = await _catalogueRepository.GetMenuItemsByRestaurantIdAsync(RestaurantId.Create(request.RestaurantId));

            // Map to response DTO
            var response = new CatalogueResponse(
                RestaurantId: request.RestaurantId,
                MenuItems: menuItems.Select(item => new MenuItemDto(
                    MenuItemId: item.Id.Value,
                    Name: item.Name,
                    Description: item.Description,
                    Price: item.Price.Amount,
                    Category: (int)item.Category
                )).ToList()
            );

            return response;
        }
        #endregion

    }


}
