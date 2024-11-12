using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infrastructure.Messaging.Implementation.RabbitMQ
{
    public class RabbitMQueueSubscriber : RabbitMQueueBase, IMessageSubscriber
    {
        private EventingBasicConsumer? _consumer;
        private string _queueName = string.Empty;
        private RabbitMQueueSubscriber(IConnection connection) : base(connection)
        {

        }

        public override void Configure(QueueConfiguration config)
        {
            if (_initialized) return;
            base.Configure(config);
            _queueName = config.QueueName;
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

        public static RabbitMQueueSubscriber Create()
        {
            var factory = QueueConnectionFactory.GetFactory();
            return new RabbitMQueueSubscriber(factory.CreateConnection());  
        }
    }
}
