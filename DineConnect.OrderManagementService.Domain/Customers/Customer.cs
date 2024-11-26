using DineConnect.OrderManagementService.Domain.Common;
using DineConnect.OrderManagementService.Domain.Customers.Entities;
using DineConnect.OrderManagementService.Domain.Customers.Events;
using DineConnect.OrderManagementService.Domain.Customers.ValueObjects;

namespace DineConnect.OrderManagementService.Domain.Customers
{
    public class Customer : AggregateRoot<CustomerId, Guid>
    {
        #region Private & Protected fields
        private DeliveryAddress _deliveryAddress;
        #endregion Private & Protected fields

        #region Constructors
        private Customer(CustomerId id, string name, string email, DeliveryAddress deliveryAddress) : base(id)
        {
            Name = name;
            Email = email;
            _deliveryAddress = deliveryAddress; 
        }
        private Customer() { }
        #endregion Constructors

        #region Public Properties
        public string Name { get; private set; } 
        public string Email { get; private set; }
        public DeliveryAddress? DeliveryAddress => _deliveryAddress;
        #endregion Public Properties

        #region Public Methods
        public void SetDeliveryAddress(DeliveryAddress deliveryAddress)
        {
            _deliveryAddress = deliveryAddress;
        }

        public void UpdateEmail(string newEmail)
        {
            Email = newEmail;
        }
        #endregion Public Methods

        #region Domain Events
        public void CustomerCreated()
        {
            AddDomainEvent(CustomerCreatedEvent.Create(this));
        }
        #endregion Domain Events

        #region Factory Methods
        public static Customer Create(string name, string email, string street, string city, string postalCode)
        {
            var customer = new Customer(CustomerId.Create(), name, email, DeliveryAddress.Create(street, city, postalCode));
            customer.CustomerCreated();
            return customer;
        }
       
        public static Customer Create(CustomerId id, string name, string email, string street, string city, string postalCode)
        {
            var customer = new Customer(id, name, email, DeliveryAddress.Create(street, city, postalCode));
            customer.CustomerCreated();
            return customer;
        }
        #endregion Factory Methods

    }

}
