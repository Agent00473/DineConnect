using Infrastructure.Domain.Response;
using MediatR;

namespace DineConnect.RestaurantManagementService.Application.Features.Restaurants.Query.Catalogue
{
    public record CatalogueQuery(Guid RestaurantId): IRequest<RestaurantAPIResponse<CatalogueResponse>>;
}
