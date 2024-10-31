namespace DineConnect.OrderManagementService.Contracts.Customer
{
    public record CustomerResponse
    {
        public Guid Id { get; init; } = Guid.Empty;
        public string Name { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public DeliveryAddressResponse Address { get; init; } = new DeliveryAddressResponse(Guid.Empty, string.Empty, string.Empty, string.Empty);

        public CustomerResponse(Guid id, string name, string email, DeliveryAddressResponse address)
        {
            Id = id;
            Name = name;
            Email = email;
            Address = address;
        }
        public CustomerResponse() { }
    }


    public record DeliveryAddressResponse(
    Guid Id, string street,
    string City, string PostalCode);

}

