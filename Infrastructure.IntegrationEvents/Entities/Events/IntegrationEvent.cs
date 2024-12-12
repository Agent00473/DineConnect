using Infrastructure.Messaging.Entities;

namespace Infrastructure.IntegrationEvents.Entities.Events
{
    /// <summary>
    /// Placeholder record for Integration Events
    /// </summary>
    public record IntegrationEvent : EventMessage
    {
        public IntegrationEvent() : base() { }
    }
}
