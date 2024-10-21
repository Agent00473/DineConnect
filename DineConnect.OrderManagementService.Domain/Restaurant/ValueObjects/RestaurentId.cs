using DineConnect.OrderManagementService.Domain.Common;

namespace DineConnect.OrderManagementService.Domain.Restaurant.ValueObjects
{
    public class RestaurentId : AggregateRootId<Guid>
    {
        private RestaurentId(Guid id)
        {
            IdValue = id;
        }
        public override Guid IdValue { get; protected set; }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return IdValue;
        }
        public static RestaurentId Create()
        {
            return new RestaurentId(Guid.NewGuid());
        }
        public static RestaurentId Create(Guid id)
        {
            return new RestaurentId(id);
        }
    }
}
