namespace Infrastructure.Messaging
{
    /// <summary>
    /// Record representing the configuration required to dynamically create an Exchange, Queue, and their Binding.
    /// </summary>
    /// <param name="ExchangeName">Name of the exchange</param>
    /// <param name="ExchangeType">Type of the exchange (e.g., "direct", "topic", "fanout")</param>
    /// <param name="QueueName">Name of the queue</param>
    /// <param name="RoutingKey">Routing key for binding</param>
    /// <param name="IsExchangeDurable"></param>
    /// <param name="IsQueueDurable"></param>
    public record QueueConfiguration(
         string ExchangeName,         
         string ExchangeType,         
         string QueueName,            
         string RoutingKey,          
         bool IsExchangeDurable,
        bool IsQueueDurable
     );

}
