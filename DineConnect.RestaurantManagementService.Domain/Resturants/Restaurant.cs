using DineConnect.RestaurantManagementService.Domain.Catalogues.ValueObjects;
using DineConnect.RestaurantManagementService.Domain.Orders.ValueObjects;
using Infrastructure.Domain.Entities;

namespace DineConnect.RestaurantManagementService.Domain.Resturants
{
    public class Restaurant : BaseAggregateRoot<RestaurantId, Guid>
    {
        #region Constants and Static Fields
        // Add constants and static fields here
        #endregion

        #region Private & Protected Fields
        #endregion

        #region Private & Protected Methods
        // Add protected methods here
        #endregion


        #region Constructors
        public Restaurant(RestaurantId id, CustomerId customerId, CatalogueId catalogueId): base(id)
        {
            CustomerId = customerId;
            CatalogueId = catalogueId;
        }
        #endregion

        #region Public Properties
        public string Name { get; private set; }
        public CustomerId CustomerId { get; private set; }
        public CatalogueId CatalogueId { get; private set; }

        #endregion

        #region Public Methods
        #endregion

        #region Interface Implementations
        #endregion

        #region Factory Methods
        // Factory Method
        public static Restaurant Create(CustomerId customerId, CatalogueId catalogueId)
        {
            return new Restaurant(RestaurantId.Create(), customerId, catalogueId);
        }
        #endregion
    }
}
