namespace DineConnect.RestaurantManagementService.Application.Features.Restaurants.Query.Catalogue
{
    public record CatalogueResponse(Guid RestaurantId, List<MenuItemDto> MenuItems);

    public record MenuItemDto(Guid MenuItemId, string Name, string Description, decimal Price, int Category);
}
