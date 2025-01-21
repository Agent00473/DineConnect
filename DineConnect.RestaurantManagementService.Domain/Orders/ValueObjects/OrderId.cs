using Infrastructure.Domain.Entities;

namespace DineConnect.RestaurantManagementService.Domain.Orders.ValueObjects
{
    public class OrderId : BaseAggregateRootId<Guid>
    {
        private OrderId(Guid value)
        {
            IdValue = value;
        }

        private OrderId()
        {
        }

        public override Guid IdValue { get; protected set; }


        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return IdValue;
        }

        public static OrderId Create()
        {
            return new(Guid.NewGuid());
        }

        public static OrderId Create(Guid value)
        {
            return new(value);
        }
    }

}
