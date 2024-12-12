using System.Text.Json.Serialization;

namespace Infrastructure.IntegrationEvents.Entities.Events
{
    public record HeartBeatEvent([property: JsonInclude] string Message) : IntegrationEvent;
}
