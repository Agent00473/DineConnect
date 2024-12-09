using System.Text.Json.Serialization;

namespace Infrastructure.IntegrationEvents.Events
{
    public record HeartBeatEvent([property: JsonInclude] string Message) : IntegrationEvent;
}
