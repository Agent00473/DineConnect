using Infrastructure.Domain.Entities;

namespace DineConnect.RestaurantManagementService.Domain.Orders.ValueObjects
{
    public class CustomerId : BaseValueObject
    {
        private CustomerId(Guid value) : base()
        {
            Value = value;
        }

        private CustomerId()
        {
        }

        public Guid Value { get; }


        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static CustomerId Create()
        {
            return new(Guid.NewGuid());
        }

        public static CustomerId Create(Guid value)
        {
            return new(value);
        }
    }

}
