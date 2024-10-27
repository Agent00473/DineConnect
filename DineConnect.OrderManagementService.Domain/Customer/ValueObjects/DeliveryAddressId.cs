using DineConnect.OrderManagementService.Domain.Common;

namespace DineConnect.OrderManagementService.Domain.Customer.ValueObjects
{
    public class DeliveryAddressId : ValueObject
    {
        private DeliveryAddressId(Guid value)
        {
            Value = value;
        }

        private DeliveryAddressId()
        {
        }

        public Guid Value { get; }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        public static DeliveryAddressId Create()
        {
            return new(Guid.NewGuid());
        }
        public static DeliveryAddressId Create(Guid value)
        {
            return new(value);
        }
    }
}
