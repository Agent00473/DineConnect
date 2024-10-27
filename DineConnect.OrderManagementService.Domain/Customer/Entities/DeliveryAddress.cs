using DineConnect.OrderManagementService.Domain.Common;
using DineConnect.OrderManagementService.Domain.Customer.ValueObjects;

namespace DineConnect.OrderManagementService.Domain.Customer.Entities
{
    public class DeliveryAddress : BaseEntity<DeliveryAddressId>
    {
        private DeliveryAddress(DeliveryAddressId id, string street, string city, string postalCode): base(id)
        {
            Street = street;
            City = city;
            PostalCode = postalCode;
        }
        private DeliveryAddress()
        {

        }

        public string Street { get; private set; }
        public string City { get; private set; }
        public string PostalCode { get; private set; }

        public static DeliveryAddress Create(string street, string city, string postalCode)
        {
            return new DeliveryAddress(DeliveryAddressId.Create(Guid.NewGuid()), street, city, postalCode);
        }
        public static DeliveryAddress Create(DeliveryAddressId id, string street, string city, string postalCode)
        {
            return new DeliveryAddress(id, street, city, postalCode);
        }

    }

}
