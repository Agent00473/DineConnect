using DineConnect.PaymentManagementService.Domain.Common;
using DineConnect.PaymentManagementService.Domain.Invoice.ValueObjects;
using DineConnect.PaymentManagementService.Domain.Payment.ValueObjects;

namespace DineConnect.PaymentManagementService.Domain.Invoice
{
    public class Invoice : AggregateRoot<InvoiceId, Guid>
    {
        #region Private & Protected
        private PaymentId? _paymentId;
        #endregion

        #region Constructors
        private Invoice(InvoiceId id, decimal totalAmount, DateTime dueDate) : base(id)
        {
            InvoiceDate = DateTime.UtcNow;
            TotalAmount = totalAmount;
            DueDate = dueDate;
            Status = Invoice_Status.Pending;
        }
        private Invoice()
        {
            
        }
        #endregion

        #region Public Properties
        public PaymentId PaymentId => _paymentId;
        public DateTime InvoiceDate { get; private set; }
        public decimal TotalAmount { get; private set; }
        public DateTime DueDate { get; private set; }
        public Invoice_Status Status { get; private set; }
        #endregion

        #region Public Methods
        public void AddPayment(PaymentId id)
        {
            if (id != null)
            {
                _paymentId = id;
            }
        }

        public void SetStatus(Invoice_Status status)
        {
            Status = status;
        }
        #endregion


        #region Factory Methods
        public static Invoice Create(InvoiceId id, decimal totalAmount, DateTime dueDate)
        {
            return new Invoice(id, totalAmount, dueDate);
        }
        public static Invoice Create(decimal totalAmount, DateTime dueDate)
        {
            return new Invoice(InvoiceId.Create(Guid.NewGuid()), totalAmount, dueDate);
        }
        #endregion
    }
}
