using Infrastructure.IntegrationEvents.Entities.Events;
using Infrastructure.Messaging.Entities;

namespace Infrastructure.IntegrationEvents.EventHandlers
{
    public interface IEventPublisher
    {
        public Task<bool> Publish<TData>(Guid transactionId) where TData : IntegrationEvent;
        public Task<bool> PublishAll<TData>() where TData : IntegrationEvent;
        public Task<bool> AddPulse<TData>() where TData : EventMessage;

    }
}
