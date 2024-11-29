using System.Text.Json;

namespace Infrastructure.Messaging
{
    internal static class SerializationHelper
    {

        private static readonly JsonSerializerOptions s_caseInsensitiveOptions = new() { PropertyNameCaseInsensitive = true };
        private static readonly JsonSerializerOptions s_writeOptions = new() { WriteIndented = false };

        public static byte[] SerializeMessage<TData>(EventMessage<TData> message)
        {
            return JsonSerializer.SerializeToUtf8Bytes(message, message.GetType(), s_writeOptions);
        }
   
        public static EventMessage<TData> DeserializeMessage<TData>(byte[] data, Type type)
        {
            return JsonSerializer.Deserialize(data, type, s_caseInsensitiveOptions) as EventMessage<TData>;
        }

        public static EventMessage<TData> DeserializeMessage<TData>(byte[] data)
        {
            var result = JsonSerializer.Deserialize<EventMessage<TData>>(data, s_caseInsensitiveOptions);
            return result;
        }
    }
}
