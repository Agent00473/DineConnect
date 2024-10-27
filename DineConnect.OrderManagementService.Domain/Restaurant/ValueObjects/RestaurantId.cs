using DineConnect.OrderManagementService.Domain.Common;

namespace DineConnect.OrderManagementService.Domain.Restaurant.ValueObjects
{
    public class RestaurantId : AggregateRootId<Guid>
    {
        private RestaurantId(Guid id)
        {
            IdValue = id;
        }

        private RestaurantId()
        {
            
        }
        public override Guid IdValue { get; protected set; }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return IdValue;
        }
        public static RestaurantId Create()
        {
            return new RestaurantId(Guid.NewGuid());
        }
        public static RestaurantId Create(Guid id)
        {
            return new RestaurantId(id);
        }
    }
}
