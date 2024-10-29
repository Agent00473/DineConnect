using DineConnect.PaymentManagementService.Domain.Common;

namespace DineConnect.PaymentManagementService.Domain.Payment.ValueObjects
{
    /// <summary>
    /// Defines the various payment method types available for processing payments.
    /// </summary>
    public enum PaymentMethodType
    {
        Unknown = 0, 
        CreditCard,
        DebitCard,
        BankTransfer,
        PayPal,
        ApplePay,
        GooglePay,
        Cryptocurrency,
        Cash
    }

    public class PaymentMethod: ValueObject
    {
        public PaymentMethodType MethodType { get; private set; }

        private PaymentMethod(PaymentMethodType methodType)
        {
            MethodType = methodType;
        }

        private PaymentMethod()
        {
            
        }
        public override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }

        public static PaymentMethod Create(PaymentMethodType method)
        {
            return new PaymentMethod(method);
        }

        public static PaymentMethod Create()
        {
            return new PaymentMethod(PaymentMethodType.Cash);
        }
    }
}
