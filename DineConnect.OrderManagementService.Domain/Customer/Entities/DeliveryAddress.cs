using DineConnect.OrderManagementService.Domain.Common;
using DineConnect.OrderManagementService.Domain.Customer.ValueObjects;

namespace DineConnect.OrderManagementService.Domain.Customer.Entities
{
    public class DeliveryAddress : BaseEntity<DeliveryAddressId>
    {
        #region Constants and Static Fields
        #endregion

        #region Private Fields
        #endregion

        #region Protected Fields
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods
        #endregion

        #region Constructors
        private DeliveryAddress(DeliveryAddressId id, string street, string city, string postalCode) : base(id)
        {
            Street = street;
            City = city;
            PostalCode = postalCode;
        }
        #endregion

        #region Public Properties
        public string Street { get; private set; }
        public string City { get; private set; }
        public string PostalCode { get; private set; }
        #endregion

        #region Public Methods
        #endregion

        #region Interface Implementations
        #endregion

        #region Factory Methods
        public static DeliveryAddress Create(string street, string city, string postalCode)
        {
            return new DeliveryAddress(DeliveryAddressId.Create(), street, city, postalCode);
        }
        public static DeliveryAddress Create(DeliveryAddressId id, string street, string city, string postalCode)
        {
            return new DeliveryAddress(id, street, city, postalCode);
        }

        #endregion
    }

}
