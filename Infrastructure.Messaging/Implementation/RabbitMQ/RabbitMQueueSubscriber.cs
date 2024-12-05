using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infrastructure.Messaging.Implementation.RabbitMQ
{
    /// <summary>
    /// Message Consumer
    /// </summary>
    public class RabbitMQueueSubscriber : RabbitMQueueBase, IMessageSubscriber
    {
        private EventingBasicConsumer? _consumer;
        private string _queueName = string.Empty;
        private RabbitMQueueSubscriber(string queueName, IConnection connection) : base(connection)
        {
            _queueName = queueName;
            _consumer = new EventingBasicConsumer(Channel);
        }

        public void AddListener(EventHandler<BasicDeliverEventArgs> handler)
        {
            if (_consumer == null) throw new NullReferenceException("Consumer not configured");
            _consumer.Received += handler;
            Channel.BasicConsume(queue: _queueName, autoAck: true, consumer: _consumer, noLocal: false, exclusive: false, consumerTag: Guid.NewGuid().ToString(), arguments: new Dictionary<string, object>());
        }

        public void RemoveListener(EventHandler<BasicDeliverEventArgs> handler)
        {
            if (_consumer != null)
                _consumer.Received -= handler;
        }

        public static RabbitMQueueSubscriber Create(string queueName, IRabbitMQConfigurationManager configurationManager)
        {
            var connection = configurationManager.GetConnection();
            return new RabbitMQueueSubscriber(queueName, connection);  
        }

      
    }
}
