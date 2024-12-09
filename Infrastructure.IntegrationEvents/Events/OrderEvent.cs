using System.Text.Json.Serialization;

namespace Infrastructure.IntegrationEvents.Events
{

    public record OrderEvent : IntegrationEvent
    {
        [JsonInclude]
        public Guid OrderId { get; set; }

        [JsonInclude]
        public string Name { get; set; }

        [JsonInclude]
        public Guid CustomerId { get; set; }

        [JsonInclude]
        public EventActionCategory Category { get; set; }

        public OrderEvent(Guid orderId, Guid customerId, string name, EventActionCategory category) : base()
        {
            OrderId = orderId;
            CustomerId = customerId;
            Name = name;
            Category = category;
        }
    }
}
