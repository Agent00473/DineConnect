using DineConnect.RestaurantManagementService.Domain.Orders.ValueObjects;
using Infrastructure.Domain.Entities;

namespace DineConnect.RestaurantManagementService.Domain.Orders.Entities
{
    public class Customer : BaseEntity<CustomerId>
    {
        #region Constants and Static Fields
        #endregion

        #region Private & Protected Fields
        #endregion

        #region Private & Protected Methods

        #endregion

        #region Constructors
        private Customer(CustomerId id, string name) : base(id)
        {
            CustomerName = name;
        }
        #endregion

        #region Public Properties
        public string CustomerName { get; private set; } = string.Empty;

        #endregion

        #region Public Methods
        #endregion

        #region Factory Methods
        public static Customer Create(string name)
        {
            return new Customer(CustomerId.Create(), name);
        }
        public static Customer Create(CustomerId customerId, string name)
        {
            return new Customer(customerId, name);
        }
        #endregion
    }
}
