using DineConnect.PaymentManagementService.Domain.Common;
using DineConnect.PaymentManagementService.Domain.Payment.ValueObjects;
using System.Transactions;

namespace DineConnect.PaymentManagementService.Domain.Payment.Entities
{
    /// <summary>
    /// Represents a transaction as part of the payment process, containing details such as date, amount, status, and gateway.
    /// Manages the transaction state independently within the payment context.
    /// </summary>
    public class PaymentTransaction: BaseEntity<TransactionId>
    {
        #region Private & Protected
        #endregion

        #region Constructors
        private PaymentTransaction(TransactionId id, decimal amount, Payment_Gateway gateway, Transaction_Category category) : base(id)
        {
            TransactionDate = DateTime.UtcNow;
            TransactionAmount = amount;
            Gateway = gateway;
            Status = Transaction_Status.Pending;
            Category = category;
        }

        private PaymentTransaction()
        { }
        #endregion

        #region Public Properties
        public DateTime TransactionDate { get; private set; }
        public decimal TransactionAmount { get; private set; }
        public Transaction_Status Status { get; private set; }
        public Payment_Gateway Gateway { get; private set; }

        public Transaction_Category Category { get; private set; }
        #endregion

        #region Public Methods
        public void SetStatus(Transaction_Status status)
        {
            Status = status;
        }
        #endregion

        #region Factory Methods
        public static PaymentTransaction Create(TransactionId id, decimal amount, Payment_Gateway gateway, Transaction_Category category)
        {
            return new PaymentTransaction(id, amount, gateway, category);
        }
        public static PaymentTransaction Create(decimal amount, Payment_Gateway gateway, Transaction_Category category)
        {
            return new PaymentTransaction(TransactionId.Create(Guid.NewGuid()), amount, gateway, category);
        }
        #endregion
    }
}
