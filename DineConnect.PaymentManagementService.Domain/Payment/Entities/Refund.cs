using DineConnect.PaymentManagementService.Domain.Common;
using DineConnect.PaymentManagementService.Domain.Invoice.ValueObjects;
using DineConnect.PaymentManagementService.Domain.Payment.ValueObjects;


namespace DineConnect.PaymentManagementService.Domain.Payment.Entities
{
    public class Refund: BaseEntity<RefundId>
    {
        #region Private & Protected Fields
        private readonly TransactionId _transactionId;
        #endregion

        #region Constructors
        private Refund(RefundId id, TransactionId transactionId, InvoiceId invoiceId, decimal refundAmount, Refund_Reason reason, Refund_Status status) : base(id)
        {
            RefundAmount = refundAmount;
            RefundDate = DateTime.UtcNow;
            Reason = reason;
            Status = status;
            _transactionId = transactionId;
            InvoiceId = invoiceId;
        }

        private Refund()
        { }
        #endregion

        #region Public Properties

        public decimal RefundAmount { get; private set; }
        public DateTime RefundDate { get; private set; }
        public Refund_Reason Reason { get; private set; }
        public Refund_Status Status { get; private set; }
        public TransactionId TransactionId => _transactionId;
        public InvoiceId InvoiceId { get; private set; }

        #endregion

        #region Public Methods
        public void SetStatus(Refund_Status status)
        {
            Status = status;
        }
        #endregion

        #region Factory Methods
        public static Refund Create(RefundId id, TransactionId transactionId, InvoiceId invoiceId, decimal refundAmount, Refund_Reason reason, Refund_Status status)
        {
            return new Refund(id, transactionId, invoiceId, refundAmount, reason, status);
        }
        public static Refund Create(TransactionId transactionId, InvoiceId invoiceId, decimal refundAmount, Refund_Reason reason)
        {
            return new Refund(RefundId.Create(), transactionId, invoiceId, refundAmount, reason, Refund_Status.Pending);
        }

        #endregion
    }
}
