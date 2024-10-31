using DineConnect.OrderManagementService.Domain.Common;

namespace DineConnect.OrderManagementService.Domain.Orders.ValueObjects
{
    public sealed class OrderItemId : AggregateRootId<Guid>
    {
        private OrderItemId(Guid value)
        {
            IdValue = value;
        }
        private OrderItemId()
        {
        }

        public override Guid IdValue { get; protected set; }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return IdValue;
        }
        public static OrderItemId Create()
        {
            return new(Guid.NewGuid());
        }
        public static OrderItemId Create(Guid value)
        {
            return new(value);
        }
    }
}
