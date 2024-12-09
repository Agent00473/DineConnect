using System.Text.Json;

namespace Infrastructure.IntegrationEvents.Events
{
    /// <summary>
    /// Integration Event Serializer for outbox table
    /// </summary>
    internal static class EventSerializer
    {
        private static readonly JsonSerializerOptions s_indentedOptions = new() { WriteIndented = true };
        private static readonly JsonSerializerOptions s_caseInsensitiveOptions = new() { PropertyNameCaseInsensitive = true };
        private static readonly JsonSerializerOptions s_writeOptions = new() { WriteIndented = false };


        public static string Serialize(IntegrationEvent entity)
        {
            return JsonSerializer.Serialize(entity, entity.GetType(), s_indentedOptions);
        }

        public static IntegrationEvent DeSerialize(string json, Type type)
        {
            return JsonSerializer.Deserialize(json, type, s_caseInsensitiveOptions) as IntegrationEvent;
        }
    }
}
