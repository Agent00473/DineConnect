using DineConnect.PaymentManagementService.Domain.Common;

namespace DineConnect.PaymentManagementService.Domain.Invoice.ValueObjects
{
    public class InvoiceId: AggregateRootId<Guid>
    {
        private InvoiceId(Guid idValue)
        {
            IdValue = idValue;
        }

        private InvoiceId()
        {
            
        }
        public override Guid IdValue { get; protected set; }

        #region Factory Methods
        public static InvoiceId Create(Guid id)
        {
            return new InvoiceId(id);
        }
        public static InvoiceId Create()
        {
            return new InvoiceId(Guid.NewGuid());
        }

        #endregion
    }
}
