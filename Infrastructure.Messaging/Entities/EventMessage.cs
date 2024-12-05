using System.Text.Json.Serialization;

namespace Infrastructure.Messaging.Entities
{
    /// <summary>
    /// Base Record for Event Messages
    /// </summary>
    public record EventMessage
    {
        public EventMessage()
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
