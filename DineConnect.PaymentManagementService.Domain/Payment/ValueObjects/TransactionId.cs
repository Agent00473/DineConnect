using DineConnect.PaymentManagementService.Domain.Common;

namespace DineConnect.PaymentManagementService.Domain.Payment.ValueObjects
{
    public class TransactionId: BaseEntityId<Guid>
    {
        private TransactionId(Guid id)
        {
            Id= id;
        }
        private TransactionId()
        {
            
        }
        public override Guid Id { get; protected set; }

        #region Factory Methods
        public static TransactionId Create(Guid id)
        {
            return new TransactionId(id);
        }
        public static TransactionId Create()
        {
            return new TransactionId(Guid.NewGuid());
        }
        #endregion
    }
}
