using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.IntegrationEvents.Entities
{
    public enum EventStateEnum
    {
        NotPublished = 0,
        InProgress = 1,
        Published = 2,
        PublishedFailed = 3
    }
    [Table("IntegrationEventData")]
    public class IntegrationEventDetail
    {
        private IntegrationEventDetail() { }
        public IntegrationEventDetail(IntegrationEvent data, Guid transactionId)
        {
            EventId = data.Id;
            CreationTime = data.CreationDate;
            EventTypeName = data.GetType().AssemblyQualifiedName;
            Content = EventSerializer.Serialize(data);
            State = EventStateEnum.NotPublished;
            TimesSent = 0;
            TransactionId = transactionId;
        }
        public Guid EventId { get; private set; }
        [Required]
        public string EventTypeName { get; private set; }
        [NotMapped]
        public IntegrationEvent IntegrationEvent { get; private set; }
        public EventStateEnum State { get; set; }
        public int TimesSent { get; set; }
        public DateTime CreationTime { get; private set; }
        [Required]
        public string Content { get; private set; }
        public Guid TransactionId { get; private set; }

        public IntegrationEventDetail DeserializeJsonContent(Type type)
        {
            IntegrationEvent = EventSerializer.DeSerialize(Content, type);
            return this;
        }
    }
}
