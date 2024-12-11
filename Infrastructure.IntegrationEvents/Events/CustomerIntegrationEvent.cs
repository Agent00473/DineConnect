using System.Text.Json.Serialization;

namespace Infrastructure.IntegrationEvents.Events

{
    public enum EventActionCategory
    {
        Created = 0,
        Updated = 1,
        Deleted = 2
    }

    public record CustomerIntegrationEvent : IntegrationEvent
    {
        [JsonInclude]
        public Guid CustomerId { get; set; }

        [JsonInclude]
        public string Name { get; set; }

        [JsonInclude]
        public string Email { get; set; }

        [JsonInclude]
        public EventActionCategory Category { get; set; }

        public CustomerIntegrationEvent(Guid customerId, string name, string email, EventActionCategory category) : base()
        {
            CustomerId = customerId;
            Name = name;
            Email = email;
            Category = category;
        }
    }
}
