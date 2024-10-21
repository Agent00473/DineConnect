
namespace DineConnect.OrderManagementService.Domain.Common
{
    public abstract class AggregateRoot<TId, TIdType> : BaseAuditableEntity<TId>
        where TId : AggregateRootId<TIdType>
    {
        protected AggregateRoot(TId id) : base(id)
        {
        }

    }
}
