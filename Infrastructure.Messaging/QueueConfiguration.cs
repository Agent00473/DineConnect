namespace Infrastructure.Messaging
{
    public record QueueConfiguration(
         string ExchangeName,        // Name of the exchange
         string ExchangeType,        // Type of the exchange (e.g., "direct", "topic", "fanout")
         string QueueName,           // Name of the queue
         string RoutingKey,        //  Routing key for binding
         bool IsExchangeDurable,
        bool IsQueueDurable
     );

}
