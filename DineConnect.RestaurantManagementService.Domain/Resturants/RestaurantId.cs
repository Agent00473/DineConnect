using Infrastructure.Domain.Entities;

namespace DineConnect.RestaurantManagementService.Domain.Resturants
{
    public class RestaurantId : BaseAggregateRootId<Guid>
    {
        private RestaurantId(Guid value)
        {
            IdValue = value;
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
            return new(Guid.NewGuid());
        }

        public static RestaurantId Create(Guid value)
        {
            return new(value);
        }
    }

}
