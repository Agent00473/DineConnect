
namespace RAbbitTest
{
public record RabbitMQConfig(
     string ExchangeName,        // Name of the exchange
     string ExchangeType,        // Type of the exchange (e.g., "direct", "topic", "fanout")
     string QueueName,           // Name of the queue
     string[] RoutingKeys        // List of routing keys for binding
 );

}
