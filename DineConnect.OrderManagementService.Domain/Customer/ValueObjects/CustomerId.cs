using DineConnect.OrderManagementService.Domain.Common;

namespace DineConnect.OrderManagementService.Domain.Customer.ValueObjects
{
    public class CustomerId : AggregateRootId<Guid>
    {
        private CustomerId(Guid id)
        {
            IdValue = id;
        }
        public override Guid IdValue { get; protected set; }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return IdValue;
        }

        public static CustomerId Create()
        {
            return new CustomerId(Guid.NewGuid());
        }
        public static CustomerId Create(Guid guid)
        {
            return new CustomerId(guid);
        }
    }
}
