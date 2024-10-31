using DineConnect.OrderManagementService.Contracts.Customer;
using MediatR;

namespace DineConnect.OrderManagementService.Application.Customer.Command
{
    public record CreateNewCustomersCommand(
        IEnumerable<NewCustomerRequest> Data): IRequest<IEnumerable<CustomerResponse>>;
}
