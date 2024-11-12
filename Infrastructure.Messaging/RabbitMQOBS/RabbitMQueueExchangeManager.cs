using RabbitMQ.Client;

namespace Infrastructure.Messaging.RabbitMQOBS
{
    //public class RabbitMQueueExchangeManager
    //{
    //    private readonly IConnectionFactory _connectionFactory;
    //    private IConnection _connection;
    //    private IModel _channel;

    //    public RabbitMQueueExchangeManager(string hostName, int port, string userName, string password)
    //    {
    //        _connectionFactory = new ConnectionFactory()
    //        {
    //            HostName = hostName,
    //            Port = port,
    //            UserName = userName,
    //            Password = password
    //        };
    //        Connect();
    //    }

    //    private void Connect()
    //    {
    //        _connection = _connectionFactory.CreateConnection();
    //        _channel = _connection.CreateModel();
    //    }

    //    public void CreateExchange(string exchangeName, ExchangeType exchangeType, bool durable = true, bool autoDelete = false)
    //    {
    //        _channel.ExchangeDeclare(exchange: exchangeName, type: exchangeType.ToString(), durable: durable, autoDelete: autoDelete);
    //    }

    //    public void CreateQueue(string queueName, bool durable = true, bool exclusive = false, bool autoDelete = false)
    //    {
    //        _channel.QueueDeclare(queue: queueName, durable: durable, exclusive: exclusive, autoDelete: autoDelete);
    //    }

    //    public void Dispose()
    //    {
    //        _channel?.Close();
    //        _connection?.Close();
    //    }
    //}

    //public enum ExchangeType
    //{
    //    Direct,
    //    Fanout,
    //    Topic,
    //    Headers
    //}
}


