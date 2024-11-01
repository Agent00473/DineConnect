namespace DineConnect.OrderManagementService.Application.Interfaces.Requests
{
    public interface INewCustomerRequest
    {
        INewDeliveryAddressRequest Address { get; init; }
        string email { get; init; }
        string Name { get; init; }
    }

    public interface INewDeliveryAddressRequest
    {
        string City { get; init; }
        string PostalCode { get; init; }
        string street { get; init; }
    }

}
