using DineConnect.OrderManagementService.Domain.Common;

namespace DineConnect.OrderManagementService.Domain.Orders.ValueObjects
{
    public class OrderId : AggregateRootId<Guid>
    {
        private OrderId(Guid value)
        {
            IdValue = value;
        }
        private OrderId() { }

        public override Guid IdValue { get; protected set; }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return IdValue;
        }

        public static OrderId Create()
        {
            return new OrderId(Guid.NewGuid());
        }

        public static OrderId Create(Guid value)
        {
            return new(value);
        }

    }
}
