
namespace Infrastructure.Domain.Entities
{
  public abstract class BaseAggregateRoot<TId, TIdType> : BaseEntity<TId>
        where TId : BaseAggregateRootId<TIdType>
    {
        protected BaseAggregateRoot(TId id) : base(id)
        {
        }
        protected BaseAggregateRoot()
        {
        }
    }
}
