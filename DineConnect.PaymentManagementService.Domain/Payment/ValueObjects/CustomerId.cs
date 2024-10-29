using DineConnect.PaymentManagementService.Domain.Common;

namespace DineConnect.PaymentManagementService.Domain.Payment.ValueObjects
{
    public class CustomerId : BaseEntityId<Guid>
    {
        private CustomerId(Guid id)
        {
            Id = id;
        }

        private CustomerId()
        {
        }

        #region Public Properties
        public override Guid Id { get; protected set; }
        #endregion

        #region Public Methods

        #endregion

        #region Factory Methods
        public static CustomerId Create()
        {
            return new CustomerId(Guid.NewGuid());
        }
        public static CustomerId Create(Guid value)
        {
            return new CustomerId(value);
        }
        #endregion
    }
}
