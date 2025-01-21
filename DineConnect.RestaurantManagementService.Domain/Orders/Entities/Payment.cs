using DineConnect.RestaurantManagementService.Domain.Common;
using DineConnect.RestaurantManagementService.Domain.Orders.ValueObjects;
using Infrastructure.Domain.Entities;

namespace DineConnect.RestaurantManagementService.Domain.Orders.Entities
{
    public class Payment: BaseEntity<PaymentId>
    {
        #region Constants and Static Fields
        #endregion

        #region Private & Protected Fields
        #endregion

        #region Private & Protected Methods

        #endregion

        #region Constructors
        public Payment(PaymentId id, Price amount, PaymentStatus status): base(id)
        {
            Amount = amount;
            Status = status;
        }
        #endregion

        #region Public Properties
        public PaymentStatus Status { get; private set; }
        public Price Amount { get; private set; }
        #endregion

        #region Public Methods
        #endregion

        #region Factory Methods
        public static Payment Create(Price amount)
        {
            return new Payment(PaymentId.Create(), amount, PaymentStatus.Pending);
        }

        public static Payment Create(Price amount, PaymentStatus status)
        {
            return new Payment(PaymentId.Create(), amount, status);
        }

        public static Payment Create(PaymentId id, Price amount, PaymentStatus status)
        {
            return new Payment(id, amount, status);
        }
        #endregion
    }
}
