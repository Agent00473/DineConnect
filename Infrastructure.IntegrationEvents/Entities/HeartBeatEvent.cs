using Infrastructure.IntegrationEvents.Entities;
using System.Text.Json.Serialization;

namespace Infrastructure.Messaging.Entities
{
    public record HeartBeatEvent([property: JsonInclude] string Message) : IntegrationEvent;
}
