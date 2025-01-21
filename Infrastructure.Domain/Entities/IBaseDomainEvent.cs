namespace Infrastructure.Domain.Entities
{
    public interface IBaseDomainEvent
    {
        public Guid EventId { get; }
    }

}
