using DineConnect.OrderManagementService.Domain.Common;

namespace DineConnect.OrderManagementService.Domain.Orders.ValueObjects
{
    public class Payment : ValueObject
    {
        private Payment(decimal amount, PaymentMethod paymentMethod)
        {
            Amount = amount;
            PaymentMethod = paymentMethod;
            Status = PaymentStatus.Pending;
        }
        private Payment()
        {

        }
        public decimal Amount { get; private set; }
        public PaymentMethod PaymentMethod { get; private set; }
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
        public static Payment Create(decimal amount, PaymentMethod paymentMethod)
        {
            return new Payment(amount, paymentMethod);
        }

    }

}
