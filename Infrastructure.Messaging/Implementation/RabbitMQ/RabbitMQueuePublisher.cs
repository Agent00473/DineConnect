using Infrastructure.Messaging.Common;
using Infrastructure.Messaging.Entities;
using RabbitMQ.Client;

namespace Infrastructure.Messaging.Implementation.RabbitMQ
{
    /// <summary>
    /// Message Publisher
    /// </summary>
    public class RabbitMQueuePublisher : RabbitMQueueBase, IMessagePublisher
    {
        private RabbitMQueuePublisher(IConnection connection) : base(connection)
        {
        }
        public bool SendMessage(RouteData route, EventMessage message)
        {
            try
            {
                var body = SerializationHelper.SerializeMessage(message);
                var properties = Channel.CreateBasicProperties();
                Console.WriteLine($"Exchange : {route.ExchangeName} RouteKey : {route.RouteKey}");
                Channel.BasicPublish(route.ExchangeName, route.RouteKey, true, properties, body);
                return Channel.WaitForConfirms();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public static RabbitMQueuePublisher Create(IRabbitMQConfigurationManager configurationManager)
        {
            var connection = configurationManager.GetConnection();
            return new RabbitMQueuePublisher(connection);
        }
    }

}
