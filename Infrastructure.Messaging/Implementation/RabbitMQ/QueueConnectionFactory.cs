using Infrastructure.Messaging.Implementation.RabbitMQ.Configs;
using RabbitMQ.Client;

namespace Infrastructure.Messaging.Implementation.RabbitMQ
{
    internal static class QueueConnectionFactory
    {
        private static ConnectionFactory? _connectionFactory;
        public static ConnectionFactory GetFactory(RabbitMQSettings rabbitMQ)
        {
            if(_connectionFactory == null)
            {
                _connectionFactory = new ConnectionFactory
                {
                    HostName = rabbitMQ.HostName,
                    Port = rabbitMQ.Port,
                };
            }
            return _connectionFactory;
        }
    }
}
