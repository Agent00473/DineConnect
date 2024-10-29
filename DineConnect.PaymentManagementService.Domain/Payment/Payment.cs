using DineConnect.PaymentManagementService.Domain.Common;
using DineConnect.PaymentManagementService.Domain.Invoice.ValueObjects;
using DineConnect.PaymentManagementService.Domain.Payment.Entities;
using DineConnect.PaymentManagementService.Domain.Payment.ValueObjects;

namespace DineConnect.PaymentManagementService.Domain.Payment
{
    public class Payment: AggregateRoot<PaymentId, Guid>
    {
        #region Private & Protected
        private readonly List<PaymentTransaction> _transactions;

        #endregion

        #region Constructors
        private Payment(PaymentId id, InvoiceId invoiceId, decimal amount, DateTime paymentDate, PaymentMethod method, CustomerId customerId): base(id)
        {
            Amount = amount;
            PaymentDate = paymentDate;
            Method = method;
            Status = Payment_Status.Pending;
            CustomerId = customerId;
            _transactions = new List<PaymentTransaction>();
            InvoiceId = invoiceId;
        }
        private Payment()
        {
            
        }
        #endregion

        #region Public Properties
        public InvoiceId InvoiceId { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime PaymentDate { get; private set; }
        public Payment_Status Status { get; private set; }
        public PaymentMethod Method { get; private set; }
        public IReadOnlyCollection<PaymentTransaction> Transactions => _transactions.AsReadOnly();
        
        /// <summary>
        /// Contact Order Service to Get Customer Details, GRPC Call
        /// </summary>
        public CustomerId CustomerId { get; private set; }
        #endregion

        #region Public Methods
        public void AddTransaction(PaymentTransaction transaction)
        {
            if (transaction != null)
                _transactions.Add(transaction);
        }

        public void SetStatus(Payment_Status status)
        {
            Status = status;
        }
   
        #endregion

        #region Factory Methods
        public static Payment Create(PaymentId id, InvoiceId invoiceId, decimal amount, DateTime paymentDate, PaymentMethod method, CustomerId customerid)
        {
            return new Payment(id, invoiceId, amount, paymentDate, method, customerid);
        }

        public static Payment Create(InvoiceId invoiceId, decimal amount, DateTime paymentDate, PaymentMethod method, CustomerId customerid)
        {
            return new Payment(PaymentId.Create(Guid.NewGuid()), invoiceId, amount, paymentDate, method, customerid);
        }
        #endregion
    }
}
