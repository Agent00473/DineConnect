using RabbitMQ.Client;

namespace RAbbitTest
{
    public abstract class RabbitMQueueBase: IDisposable
    {
        private readonly IConnection _connection;
        private IModel _channel;
        private bool disposedValue;
        protected bool _initialized = false;
        protected string _exchangeName = string.Empty;

        protected IModel Channel
        {
            get
            {
                if (_channel == null)
                {
                    _channel = _connection.CreateModel();
                }
                return _channel;
            }
        }

        private void DeclareExchange(IModel channel, string exchangeName, string exchangeType)
        {
            // Declare an exchange (e.g., direct, fanout, topic)
            channel.ExchangeDeclare(exchange: exchangeName, type: exchangeType);
            _exchangeName = exchangeName;
        }
        private void BindQueueToExchange(IModel channel, string queueName, string exchangeName, string routingKey)
        {
            // Bind the queue to the exchange with a specific routing key
            channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: routingKey);
        }
        private QueueDeclareOk DeclareQueue(IModel channel, string queueName)
        {
            // Declare a queue with options such as durable, exclusive, and autoDelete
            return channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false);
        }

        protected void InitializeRabbitMQueue(RabbitMQConfig config)
        {
            // Declare the exchange with the specified type
            DeclareExchange(Channel, config.ExchangeName, config.ExchangeType);
            // Declare the queue
            var queue = DeclareQueue(Channel, config.QueueName);
            // Bind the queue to the exchange with the provided routing keys
            foreach (var routingKey in config.RoutingKeys)
            {
                BindQueueToExchange(Channel, config.QueueName, config.ExchangeName, routingKey);
            }

        }

        protected RabbitMQueueBase(IConnection connection)
        {
            _connection = connection;
        }
        public virtual void Configure(RabbitMQConfig config)
        {
            if (!_initialized)
            {
                InitializeRabbitMQueue(config);
                _initialized = true;
            }
        }
        //public abstract void Cleanup();

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _connection?.Dispose();
                    _channel?.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        
        //Optionl added to handle any missed cleanups
        ~RabbitMQueueBase()
        {
            Dispose(disposing: false);
        }

    }
}
