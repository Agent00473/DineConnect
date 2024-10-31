namespace DineConnect.OrderManagementService.Contracts.Customer
{
    public record NewCustomerRequest(
        string Name,
        string email, CreateDeliveryAddressRequest Address);

    public record CreateDeliveryAddressRequest(
        string street,
        string City, string PostalCode);

}

