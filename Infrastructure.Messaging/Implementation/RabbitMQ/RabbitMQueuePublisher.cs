using Infrastructure.Messaging.Entities;
using RabbitMQ.Client;

namespace Infrastructure.Messaging.Implementation.RabbitMQ
{
    public class RabbitMQueuePublisher : RabbitMQueueBase, IMessagePublisher
    {
        private RabbitMQueuePublisher(IConnection connection, string exchangeName) : base(connection)
        {
            _exchangeName = exchangeName;
        }
        public bool SendMessage(string routingkey, EventMessage message)
        {
            try
            {
                var body = SerializationHelper.SerializeMessage(message);
                var properties = Channel.CreateBasicProperties();
                Console.WriteLine($"Exchange : {_exchangeName} RouteKey : {routingkey}");
                Channel.BasicPublish(_exchangeName, routingkey, true, properties, body);
                return Channel.WaitForConfirms();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public static RabbitMQueuePublisher Create(string exchangeName, IRabbitMQConfigurationManager configurationManager)
        {
            var connection = configurationManager.GetConnection();
            return new RabbitMQueuePublisher(connection, exchangeName);
        }
    }

}
