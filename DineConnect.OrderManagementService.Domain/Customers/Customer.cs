using DineConnect.OrderManagementService.Domain.Common;
using DineConnect.OrderManagementService.Domain.Customers.Entities;
using DineConnect.OrderManagementService.Domain.Customers.ValueObjects;

namespace DineConnect.OrderManagementService.Domain.Customers
{
    public class Customer : AggregateRoot<CustomerId, Guid>
    {
        private DeliveryAddress _deliveryAddress;
        private Customer(CustomerId id, string name, string email, DeliveryAddress deliveryAddress) : base(id)
        {
            Name = name;
            Email = email;
            _deliveryAddress = deliveryAddress; 
        }
        private Customer() { }
        public string Name { get; private set; } 
        public string Email { get; private set; }
        public DeliveryAddress? DeliveryAddress => _deliveryAddress; 
        public void SetDeliveryAddress(DeliveryAddress deliveryAddress)
        {
            _deliveryAddress = deliveryAddress;
        }

        public void UpdateEmail(string newEmail)
        {
            Email = newEmail;
        }
        public static Customer Create(string name, string email, string street, string city, string postalCode)
        {
            return new Customer(CustomerId.Create(), name, email, DeliveryAddress.Create(street, city, postalCode));
        }

        public static Customer Create(CustomerId id, string name, string email, string street, string city, string postalCode)
        {
            return new Customer(id, name, email, DeliveryAddress.Create(street, city, postalCode));
        }
    }

}
