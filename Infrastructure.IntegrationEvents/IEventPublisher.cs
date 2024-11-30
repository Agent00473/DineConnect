
using Infrastructure.IntegrationEvents.Entities;

namespace Infrastructure.IntegrationEvents
{
    public interface IEventPublisher
    {
        public Task<bool> Publish<TData>(Guid transactionId) where TData : IntegrationEvent;
    }
}
