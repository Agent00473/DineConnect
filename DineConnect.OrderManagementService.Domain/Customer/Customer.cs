using DineConnect.OrderManagementService.Domain.Common;
using DineConnect.OrderManagementService.Domain.Customer.Entities;
using DineConnect.OrderManagementService.Domain.Customer.ValueObjects;

namespace DineConnect.OrderManagementService.Domain.Customer
{
    public class Customer : AggregateRoot<CustomerId, Guid>
    {
        #region Constants and Static Fields
        // Add any constants or static fields here, e.g., default values
        #endregion

        #region Private Fields
        private DeliveryAddress _deliveryAddress;
        #endregion

        #region Protected Fields
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods
        #endregion

        #region Constructors
        private Customer(CustomerId customerId, string name, string email, DeliveryAddress deliveryAddress) : base(customerId)
        {
            Name = name;
            Email = email;
            _deliveryAddress = deliveryAddress; 
        }

        #endregion

        #region Public Properties
        public string Name { get; private set; } 
        public string Email { get; private set; }
        public DeliveryAddress DeliveryAddress => _deliveryAddress; 
        #endregion

        #region Public Methods
        public void SetDeliveryAddress(DeliveryAddress deliveryAddress)
        {
            _deliveryAddress = deliveryAddress;
        }

        public void UpdateEmail(string newEmail)
        {
            Email = newEmail;
        }
        #endregion

        #region Interface Implementations
        #endregion

        #region Factory Methods
        public static Customer Create(string name, string email, string street, string city, string postalCode)
        {
            return new Customer(CustomerId.Create(), name, email, DeliveryAddress.Create(street, city, postalCode));
        }

        public static Customer Create(CustomerId id, string name, string email, string street, string city, string postalCode)
        {
            return new Customer(id, name, email, DeliveryAddress.Create(street, city, postalCode));
        }

        #endregion
    }

}
