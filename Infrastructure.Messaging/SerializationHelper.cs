using System.Text.Json;

namespace Infrastructure.Messaging
{
    internal static class SerializationHelper
    {
        private static JsonSerializerOptions _options = new JsonSerializerOptions { WriteIndented = false };

        public static byte[] SerializeMessage<TData>(EventMessage<TData> message)
        {
            return JsonSerializer.SerializeToUtf8Bytes(message, message.GetType(), _options);
        }

        public static EventMessage<TData>? DeserializeMessage<TData>(byte[] data, Type eventType)
        {
            return JsonSerializer.Deserialize(data, eventType, _options) as EventMessage<TData>;
        }
    }
}
