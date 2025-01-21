using Infrastructure.Domain.Entities;

namespace DineConnect.RestaurantManagementService.Domain.Catalogues.ValueObjects
{
    public class MenuItemId : BaseValueObject
    {
        private MenuItemId(Guid value)
        {
            Value = value;
        }

        private MenuItemId()
        {
        }

        public Guid Value { get; }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static MenuItemId Create()
        {
            return new(Guid.NewGuid());
        }

        public static MenuItemId Create(Guid value)
        {
            return new(value);
        }
    }

}
