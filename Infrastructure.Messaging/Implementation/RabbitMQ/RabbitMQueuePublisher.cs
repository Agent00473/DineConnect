using RabbitMQ.Client;
using System.Threading.Channels;

namespace Infrastructure.Messaging.Implementation.RabbitMQ
{
    public class RabbitMQueuePublisher<TData> : RabbitMQueueBase, IMessagePublisher<TData>
    {
        private RabbitMQueuePublisher(IConnection connection) : base(connection)
        {
        }
        public bool SendMessage(string routingkey, EventMessage<TData> message)
        {
            try
            {
                var body = SerializationHelper.SerializeMessage(message);
                var properties = Channel.CreateBasicProperties();
                Channel.BasicPublish(_exchangeName, routingkey, true, properties, body);
                return Channel.WaitForConfirms();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public override void Configure(QueueConfiguration config)
        {
            if (_initialized) return;
            base.Configure(config);
        }

        public static RabbitMQueuePublisher<TData> Create() 
        {
            var factory = QueueConnectionFactory.GetFactory();
            return new RabbitMQueuePublisher<TData>(factory.CreateConnection());
        }

    }
}
