
using DineConnect.OrderManagementService.Domain.Common;
using DineConnect.OrderManagementService.Domain.Order.ValueObjects;

namespace DineConnect.OrderManagementService.Domain.Order.Entities
{
    internal class Payment : BaseEntity<Guid>
    {
        #region Constants and Static Fields
        #endregion

        #region Private Fields
        #endregion

        #region Protected Fields
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods
        #endregion

        #region Constructors
        public Payment(Guid paymentId, decimal amount, string paymentMethod) : base(paymentId)
        {
            Amount = amount;
            PaymentMethod = paymentMethod;
            Status = PaymentStatus.Pending;
        }
        #endregion

        #region Public Properties
        public decimal Amount { get; private set; }
        public string PaymentMethod { get; private set; }
        public PaymentStatus Status { get; private set; }
        #endregion

        #region Public Methods
        public void CompletePayment()
        {
            Status = PaymentStatus.Completed;
        }
        #endregion

        #region Interface Implementations
        #endregion

        #region Factory Methods
        public static Payment Create(decimal amount, string paymentMethod)
        {
            var paymentId = Guid.NewGuid();
            return new Payment(paymentId, amount, paymentMethod);
        }

        public static Payment Create(Guid paymentId, decimal amount, string paymentMethod)
        {
            return new Payment(paymentId, amount, paymentMethod);
        }
        #endregion
    }

}
