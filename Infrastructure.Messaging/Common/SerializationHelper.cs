using Infrastructure.Messaging.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Infrastructure.Messaging.Common
{
    /// <summary>
    /// Event Message Serilization Helpers 
    /// </summary>
    internal static class SerializationHelper
    {

        private static readonly JsonSerializerOptions s_caseInsensitiveOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        private static readonly JsonSerializerOptions s_writeOptions = new() { WriteIndented = false };

        //private static EventMessage DeSerialize(string json, Type type)
        //{
        //    return JsonSerializer.Deserialize(json, type, s_caseInsensitiveOptions) as IntegrationEvent;

        //}
        public static byte[] SerializeMessage(EventMessage message)
        {
            return JsonSerializer.SerializeToUtf8Bytes(message, message.GetType(), s_writeOptions);
        }

        public static EventMessage? DeserializeMessage(byte[] data, Type type)
        {
            return JsonSerializer.Deserialize(data, type, s_caseInsensitiveOptions) as EventMessage;
        }


        //public static EventMessage DeserializeMessage(byte[] data)
        //{
        //    var eventMessage = JsonSerializer.Deserialize<EventMessage>(data, s_caseInsensitiveOptions);
        //    if (eventMessage == null) throw new Exception("Invalid message format.");

        //    var dataType = Type.GetType(eventMessage.EventTypeName);
        //    if (dataType == null) throw new Exception($"Type not found: {eventMessage.EventTypeName}");

        //    var msgData = JsonSerializer.Deserialize(eventMessage.Data, dataType);
        //    eventMessage.Data = (EventMessageData)msgData!; // Cast to base type
        //    return eventMessage;

        //    //var result = JsonSerializer.Deserialize<EventMessage>(data, s_caseInsensitiveOptions);
        //    //return result;
        //}
    }
}
