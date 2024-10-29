
namespace DineConnect.PaymentManagementService.Domain.Common
{
    public abstract class AggregateRootId<TId> : ValueObject
    {
        public abstract TId IdValue { get; protected set; }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return IdValue!;
        }
    }
}
