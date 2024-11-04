
namespace DineConnect.OrderManagementService.Application.Features.Customers.Query
{
    public record CustomerResponse
    {
        public Guid Id { get; init; } = Guid.Empty;
        public string Name { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public AddressResponse Address { get; init; } = new AddressResponse();

        public CustomerResponse(Guid id, string name, string email, AddressResponse address)
        {
            Id = id;
            Name = name;
            Email = email;
            Address = address;
        }
        public CustomerResponse() { }

    }

    public record AddressResponse(Guid Id, string street, string City, string PostalCode)
    {
        public AddressResponse() : this(Guid.Empty, string.Empty, string.Empty, string.Empty)
        {
        }
    }

}

