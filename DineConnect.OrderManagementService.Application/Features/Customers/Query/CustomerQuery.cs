using MediatR;

namespace DineConnect.OrderManagementService.Application.Features.Customers.Query
{

    public record CustomerQuery(int PageNumber = 1, int PageSize = 10) : IRequest<CustomerResponseWrapper>
    {

    }
}
