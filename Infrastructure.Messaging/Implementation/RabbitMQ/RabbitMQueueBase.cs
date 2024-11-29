using RabbitMQ.Client;

namespace Infrastructure.Messaging.Implementation.RabbitMQ
{
    public abstract class RabbitMQueueBase : IDisposable, IMessageServiceBase
    {
        #region Private & Protected Fields
        private readonly IConnection _connection;
        private IModel _channel;
        private bool disposedValue;
        protected bool _initialized = false;
        protected string _exchangeName = string.Empty;
        #endregion

        #region Protected Methods
        protected void InitializeRabbitMQueue(QueueConfiguration config)
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
        #endregion

        #region Private Methods
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

        #endregion

        #region Constructors
        protected RabbitMQueueBase(IConnection connection)
        {
            _connection = connection;
        }
        #endregion

        #region Public & Protected Properties
        protected IModel Channel
        {
            get
            {
                if (_channel == null)
                {
                    _channel = _connection.CreateModel();
                    _channel.ConfirmSelect();
                }

                return _channel;
            }
        }
        #endregion

        #region Public Methods
        public virtual void Configure(QueueConfiguration config)
        {
            if (!_initialized)
            {
                InitializeRabbitMQueue(config);
                _initialized = true;
            }
        }
        #endregion

        #region IDispose Implementations
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
        #endregion
    }
}

