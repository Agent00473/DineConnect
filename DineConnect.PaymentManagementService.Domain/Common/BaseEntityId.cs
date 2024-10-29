
namespace DineConnect.PaymentManagementService.Domain.Common
{
    public abstract class BaseEntityId<TId> : ValueObject
    {
        public abstract TId Id { get; protected set; }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id!;
        }
    }
}
