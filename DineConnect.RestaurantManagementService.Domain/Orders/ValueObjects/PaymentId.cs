using Infrastructure.Domain.Entities;

namespace DineConnect.RestaurantManagementService.Domain.Orders.ValueObjects
{
    public class PaymentId : BaseValueObject
    {
        private PaymentId(Guid value)
        {
            Value = value;
        }

        private PaymentId()
        {
        }

        public Guid Value { get; }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static PaymentId Create()
        {
            return new(Guid.NewGuid());
        }

        public static PaymentId Create(Guid value)
        {
            return new(value);
        }
    }

}
