namespace Infrastructure.Domain.Entities
{
    public abstract class BaseAggregateRootId<TId> : BaseValueObject
    {
        public abstract TId IdValue { get; protected set; }
    }
}
