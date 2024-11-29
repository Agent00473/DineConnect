using Infrastructure.IntegrationEvents.Entities;
using System.Text.Json.Serialization;

namespace InfraTest.Events
{
    public enum EventActionCategory
    {
        Created =0,
        Updated = 1,
        Deleted = 2
    }
 //   public record CustomerEvent(Guid CustomerId, string Name, string Email, EventActionCategory Category) : IntegrationEvent;

    public record CustomerEvent: IntegrationEvent
    {
        [JsonInclude] 
        public Guid CustomerId { get; set; }

        [JsonInclude] 
        public string Name { get; set; }

        [JsonInclude]
        public string Email { get; set; }

        [JsonInclude]
        public EventActionCategory Category { get; set; }

        public CustomerEvent(Guid customerId, string name, string email, EventActionCategory category): base()
        {
            CustomerId = customerId;
            Name = name;
            Email = email;
            Category = category;
        }
    }
}
