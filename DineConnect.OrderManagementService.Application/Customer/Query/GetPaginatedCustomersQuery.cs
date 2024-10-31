using DineConnect.OrderManagementService.Contracts.Customer;
using MediatR;

namespace DineConnect.OrderManagementService.Application.Restaurant.Query
{

    public record GetPaginatedCustomersQuery(int PageNumber = 1, int PageSize = 25) : IRequest<IEnumerable<CustomerResponse>>
    {
        
    }
}
