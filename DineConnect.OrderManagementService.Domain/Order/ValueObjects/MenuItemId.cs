using DineConnect.OrderManagementService.Domain.Common;

namespace DineConnect.OrderManagementService.Domain.Order.ValueObjects
{
    public sealed class MenuItemId : ValueObject
    {
        #region Constructors
        private MenuItemId(Guid value)
        {
            Value = value;
        }
        #endregion

        #region Public Properties
        public Guid Value { get; }
        #endregion

        #region Public Methods
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        #endregion

        #region Factory Methods
        public static MenuItemId Create()
        {
            return new(Guid.NewGuid());
        }
        public static MenuItemId Create(Guid value)
        {
            return new(value);
        }

        #endregion
    }
}
