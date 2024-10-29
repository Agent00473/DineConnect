using DineConnect.PaymentManagementService.Domain.Common;

namespace DineConnect.PaymentManagementService.Domain.Payment.ValueObjects
{
    public class PaymentId: AggregateRootId<Guid>
    {
        private PaymentId(Guid id)
        {
            IdValue = id;
        }
        private PaymentId()
        {
            
        }

        #region Public Properties
        public override Guid IdValue { get; protected set; }
        #endregion

        #region Public Methods
 
        #endregion

        #region Factory Methods
        public static PaymentId Create()
        {
            return new PaymentId(Guid.NewGuid());
        }
        public static PaymentId Create(Guid value)
        {
            return new PaymentId(value);
        }
        #endregion
    }
}
