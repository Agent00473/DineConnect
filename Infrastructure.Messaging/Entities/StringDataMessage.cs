using System.Text.Json.Serialization;

namespace Infrastructure.Messaging.Entities
{
    public record StringDataMessage([property: JsonInclude] string Data) : EventMessage;
}
