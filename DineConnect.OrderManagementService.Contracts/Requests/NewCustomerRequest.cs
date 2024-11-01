using DineConnect.OrderManagementService.Application.Interfaces.Requests;

namespace DineConnect.OrderManagementService.Contracts.Requests
{
    public record NewCustomerRequest(
            string Name,
            string email, INewDeliveryAddressRequest Address) : INewCustomerRequest;

    public record NewDeliveryAddressRequest(
        string street,
        string City, string PostalCode) : INewDeliveryAddressRequest;

}

