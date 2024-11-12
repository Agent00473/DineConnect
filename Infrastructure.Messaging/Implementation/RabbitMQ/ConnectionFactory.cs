using RabbitMQ.Client;

namespace Infrastructure.Messaging.Implementation.RabbitMQ
{
    internal static class QueueConnectionFactory
    {
        private static ConnectionFactory _connectionFactory = new ConnectionFactory
        {
            HostName = "localhost",
            Port = 5672
        };
        public static ConnectionFactory GetFactory() {
            
            return _connectionFactory;
        }
    }
}
