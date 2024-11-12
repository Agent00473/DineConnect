using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using System.Threading.Channels;

namespace Infrastructure.Messaging.RabbitMQOBS
{

    //public class RabbitMQueuePublishManager : IDisposable
    //{
    //    private readonly IConnectionFactory _connectionFactory;
    //    private IConnection _connection;
    //    private IModel _channel;

    //    // Dictionary to hold consumers and their associated queues
    //    private readonly Dictionary<string, EventingBasicConsumer> _consumers = new();

    //    public RabbitMQueuePublishManager(string hostName, int port, string userName, string password)
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

    //    // Publisher Registration
    //    public void PublishMessage(string exchangeName, string routingKey, string message)
    //    {
    //        var body = Encoding.UTF8.GetBytes(message);
    //        _channel.BasicPublish(exchange: exchangeName, routingKey: routingKey, basicProperties: null, body: body);
    //    }

    //    // Consumer Registration
    //    public void RegisterConsumer(string queueName, Action<string> messageHandler)
    //    {
    //        var consumer = new EventingBasicConsumer(_channel);
    //        consumer.Received += (model, ea) =>
    //        {
    //            var body = ea.Body.ToArray();
    //            var message = Encoding.UTF8.GetString(body);
    //            messageHandler.Invoke(message);
    //            _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
    //        };

    //        // Start consuming
    //        _channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);

    //        // Store the consumer in the dictionary
    //        _consumers[queueName] = consumer;
    //    }

    //    // Unregister Consumer
    //    public void UnregisterConsumer(string queueName)
    //    {
    //        var consumer = new EventingBasicConsumer(channel);

    //        if (_consumers.TryGetValue(queueName, out var consumer))
    //        {
    //            // Stop the consumer from receiving messages
    //            _channel.BasicCancel(consumer.ConsumerTag);
    //            _consumers.Remove(queueName);
    //        }
    //        else
    //        {
    //            throw new InvalidOperationException($"No consumer registered for queue: {queueName}");
    //        }
    //    }

    //    public void Dispose()
    //    {
    //        _channel?.Close();
    //        _connection?.Close();
    //    }
    //}
}

