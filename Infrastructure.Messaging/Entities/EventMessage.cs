using System.Text.Json.Serialization;

namespace Infrastructure.Messaging.Entities
{
    //public record EventMessage
    //{
    //    public EventMessage(EventMessageData data)
    //    {
    //        Id = Guid.NewGuid();
    //        PublishedDate = DateTime.UtcNow;
    //        Data = data;
    //        EventTypeName = data.GetType().AssemblyQualifiedName;
    //    }

    //    [JsonInclude]
    //    public Guid Id { get; set; }

    //    [JsonInclude]
    //    public DateTime PublishedDate { get; set; }
    //    [JsonInclude]
    //    public string EventTypeName { get; set; }
    //}

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
