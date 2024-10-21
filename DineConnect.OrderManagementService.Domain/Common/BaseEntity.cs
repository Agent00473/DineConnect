using DineConnect.OrderManagementService.Domain.Interfaces;

namespace DineConnect.OrderManagementService.Domain.Common
{
    public abstract class BaseEntity<TId> : IEquatable<BaseEntity<TId>> where TId : notnull
    {
        private readonly List<IDomainEvent> _domainEvents = new();

        public TId Id { get; protected set; }

        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected BaseEntity(TId id)
        {
            Id = id;
        }

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        #region Equatable
        public override bool Equals(object? obj)
        {
            return obj is BaseEntity<TId> entity && Id.Equals(entity.Id);
        }

        public bool Equals(BaseEntity<TId>? other)
        {
            return Equals((object?)other);
        }

        public static bool operator ==(BaseEntity<TId> left, BaseEntity<TId> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(BaseEntity<TId> left, BaseEntity<TId> right)
        {
            return !Equals(left, right);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        #endregion

    }

}
