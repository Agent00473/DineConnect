using DineConnect.OrderManagementService.Domain.Common;

namespace DineConnect.OrderManagementService.Domain.Order.ValueObjects
{
    public class Payment : ValueObject
    {
        private Payment(decimal amount, string paymentMethod)
        {
            Amount = amount;
            PaymentMethod = paymentMethod;
            Status = PaymentStatus.Pending;
        }
        private Payment()
        {

        }
        public decimal Amount { get; private set; }
        public string PaymentMethod { get; private set; }
        public PaymentStatus Status { get; private set; }
        public void CompletePayment()
        {
            Status = PaymentStatus.Completed;
        }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return PaymentMethod;
            yield return Status;
        }
        public static Payment Create(decimal amount, string paymentMethod)
        {
            return new Payment(amount, paymentMethod);
        }

    }

}
