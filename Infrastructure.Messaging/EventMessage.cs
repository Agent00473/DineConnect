using System.Text.Json.Serialization;

namespace Infrastructure.Messaging
{
    public record EventMessage<TData>
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
        [JsonInclude]
        public TData Data { get; set; }
    }
  
}
