namespace DineConnect.OrderManagementService.Application.Interfaces.Responses
{
    public interface ICustomerResponse
    {
        IDeliveryAddressResponse Address { get; init; }
        string Email { get; init; }
        Guid Id { get; init; }
        string Name { get; init; }
    }

    public interface IDeliveryAddressResponse
    {
        string City { get; init; }
        Guid Id { get; init; }
        string PostalCode { get; init; }
        string street { get; init; }
    }

}
