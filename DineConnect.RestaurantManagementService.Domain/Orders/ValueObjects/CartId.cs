using Infrastructure.Domain.Entities;

namespace DineConnect.RestaurantManagementService.Domain.Orders.ValueObjects
{
    public class CartId : BaseValueObject
    {
        private CartId(Guid value)
        {
            Value = value;
        }

        private CartId()
        {
        }

        public Guid Value { get; }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static CartId Create()
        {
            return new(Guid.NewGuid());
        }

        public static CartId Create(Guid value)
        {
            return new(value);
        }
    }

}
