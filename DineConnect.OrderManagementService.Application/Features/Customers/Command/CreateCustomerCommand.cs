using MediatR;

namespace DineConnect.OrderManagementService.Application.Features.Customers.Command
{
    public record CreateCustomerCommand(
        IEnumerable<CustomerCommandModel> Data) : IRequest<CustomerResponseWrapper>;
}

public record CustomerCommandModel(
        string Name, string email, AddressCommandModel Address);

public record AddressCommandModel(
    string street, string City, string PostalCode);

