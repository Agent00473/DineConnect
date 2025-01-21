using Infrastructure.Domain.Entities;

namespace DineConnect.RestaurantManagementService.Domain.Catalogues.ValueObjects
{
    public class CatalogueId : BaseAggregateRootId<Guid>
    {
        private CatalogueId(Guid value) : base()
        {
            IdValue = value;
        }

        private CatalogueId()
        {
        }

        public override Guid IdValue { get; protected set; }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return IdValue;
        }

        public static CatalogueId Create()
        {
            return new(Guid.NewGuid());
        }

        public static CatalogueId Create(Guid value)
        {
            return new(value);
        }
    }

}
