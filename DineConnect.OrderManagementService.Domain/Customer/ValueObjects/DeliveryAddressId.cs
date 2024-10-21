using DineConnect.OrderManagementService.Domain.Common;

namespace DineConnect.OrderManagementService.Domain.Customer.ValueObjects
{
    public class DeliveryAddressId : AggregateRootId<Guid>
    {
        private DeliveryAddressId(Guid id)
        {
            IdValue = id;
        }
        public override Guid IdValue { get; protected set; }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return IdValue;
        }

        public static DeliveryAddressId Create()
        {
            return new DeliveryAddressId(Guid.NewGuid());
        }
        public static DeliveryAddressId Create(Guid guid)
        {
            return new DeliveryAddressId(guid);
        }
    }
}
