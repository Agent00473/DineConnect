using MediatR;

namespace DineConnect.OrderManagementService.Application.Features.Orders.Query
{

    public record OrderQuery(int PageNumber = 1, int PageSize = 10) : IRequest<OrderResponseWrapper>
    {

    }
}
