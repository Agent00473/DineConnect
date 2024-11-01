using DineConnect.OrderManagementService.Application.Interfaces.Requests;
using DineConnect.OrderManagementService.Application.Interfaces.Responses;
using MediatR;

namespace DineConnect.OrderManagementService.Application.Handlers.Customers.Command
{
    public record CreateNewCustomersCommand(
        IEnumerable<INewCustomerRequest> Data) : IRequest<IEnumerable<Domain.Customers.Customer>>;
}
