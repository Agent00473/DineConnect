
namespace DineConnect.OrderManagementService.Domain.Common
{
    public abstract class AggregateRoot<TId, TIdType> : BaseEntity<TId>
        where TId : AggregateRootId<TIdType>
    {
        protected AggregateRoot(TId id) : base(id)
        {
        }
        protected AggregateRoot()
        {
        }
    }
}
