using MediatR;

namespace DineConnect.OrderManagementService.Application.Handlers.Customers.Query
{

    public record GetPaginatedCustomersQuery(int PageNumber = 1, int PageSize = 10) : IRequest<IEnumerable<Domain.Customers.Customer>>
    {

    }
}
