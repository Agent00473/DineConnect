using RabbitMQ.Client;
using System.Text.Json;

namespace RAbbitTest
{
    public class RabbitMQueuePublisher : RabbitMQueueBase
    {
        private JsonSerializerOptions _options = new JsonSerializerOptions { WriteIndented = false };

        private byte[] SerializeMessage(EventMessage message)
        {
            return JsonSerializer.SerializeToUtf8Bytes(message, message.GetType(), _options);
        }

        public RabbitMQueuePublisher(IConnection connection) : base(connection)
        {
        }
        public void SendMessage(string queueName, EventMessage message)
        {
            try
            {
                var body = SerializeMessage(message);
                var properties = Channel.CreateBasicProperties();
                Channel.BasicPublish(_exchangeName, queueName, true, properties, body);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public override void Configure(RabbitMQConfig config)
        {
            if (_initialized) return;
            base.Configure(config);
        }
    }
}
