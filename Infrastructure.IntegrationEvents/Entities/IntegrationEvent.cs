using System.Text.Json.Serialization;

namespace Infrastructure.IntegrationEvents.Entities
{
    public record IntegrationEvent
    {
        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        [JsonInclude]
        public Guid Id { get; set; }

        [JsonInclude]
        public DateTime CreationDate { get; set; }
    }
}
