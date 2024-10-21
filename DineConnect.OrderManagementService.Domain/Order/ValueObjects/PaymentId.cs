using DineConnect.OrderManagementService.Domain.Common;

namespace DineConnect.OrderManagementService.Domain.Order.ValueObjects
{
    internal class PaymentId : ValueObject
    {
        #region Constructors
        public PaymentId(Guid value)
        {
            Value = value;
        }
        #endregion

        #region Public Properties
        public Guid Value { get; }

        #endregion

        #region Public Methods
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        #endregion

        #region Factory Methods
        public static PaymentId Create()
        {
            return new(Guid.NewGuid());
        }
        public static PaymentId Create(Guid value)
        {
            return new(value);
        }

        #endregion
    }
}
