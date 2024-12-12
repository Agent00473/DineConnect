using global::RabbitMQ.Client;
using Infrastructure.Messaging.Common;
using Infrastructure.Messaging.Implementation.RabbitMQ.Configs;
using Microsoft.Extensions.Options;

namespace Infrastructure.Messaging.Implementation.RabbitMQ
{
    public interface IRabbitMQConfigurationManager : IDisposable
    {
        void AddQueue(QueueConfiguration configuration);
        IEnumerable<ExchangeConfig> Exchanges();
        IEnumerable<QueueConfig> Queues();
        string GetRoutingKey(string queueName);
        void Initialize();
        public IConnection GetConnection();
        public string IntegrationExchangeName { get; }
    }

    /// <summary>
    /// Manager class responsible for configuring Queues, Exchanges, and Bindings based on the defined Queue Configuration settings.
    /// </summary>
    public sealed class RabbitMQConfigurationManager : IRabbitMQConfigurationManager
    {
        private readonly QueueConfigurations _settings;
        private bool disposedValue;
        private IConnection _connection;
        private IDictionary<string, string> _queueRouteTable = new Dictionary<string, string>();

        public string IntegrationExchangeName => _settings.IntegrationExchangeName;

        public RabbitMQConfigurationManager(IOptions<QueueConfigurations> options)
        {
            _settings = options.Value;
        }

        public RabbitMQConfigurationManager(QueueConfigurations config)
        {
            _settings = config;
        }

        private void ConfigureExchangesAndQueues(IModel channel)
        {
            foreach (var exchange in _settings.RabbitMQ.Exchanges)
            {
                channel.ExchangeDeclare(exchange.Name, exchange.Type, exchange.Durable);
            }

            foreach (var queue in _settings.RabbitMQ.Queues)
            {
                ConfigureQueue(channel, queue);
            }
        }

        private void ConfigureQueue(IModel channel, QueueConfig config)
        {
            channel.QueueDeclare(config.Name, config.Durable, false, false, null);
            channel.QueueBind(config.Name, config.Exchange, config.RoutingKey);
            _queueRouteTable[config.Name] = config.RoutingKey;
        }
        private void ConfigureExchangesAndQueues(IModel channel, QueueConfiguration configuration)
        {
            channel.ExchangeDeclare(configuration.ExchangeName, configuration.ExchangeType, configuration.IsExchangeDurable);
            var config = new QueueConfig()
            {
                Durable = configuration.IsQueueDurable,
                Exchange = configuration.ExchangeName,
                Name = configuration.QueueName,
                RoutingKey = configuration.RoutingKey
            };
            ConfigureQueue(channel, config);

            channel.QueueDeclare(configuration.QueueName, configuration.IsQueueDurable, false, false, null);
            channel.QueueBind(configuration.QueueName, configuration.ExchangeName, configuration.RoutingKey);
        }

        public IConnection GetConnection()
        {
            if (_connection == null)
            {
                var factory = QueueConnectionFactory.GetFactory(_settings.RabbitMQ);
                _connection = factory.CreateConnection();
            }
            return _connection;
        }

        public void Initialize()
        {
            var connection = GetConnection();
            using (var channel = connection.CreateModel())
            {
                ConfigureExchangesAndQueues(channel);
            }
        }

        public void AddQueue(QueueConfiguration configuration)
        {
            var connection = GetConnection();
            using (var channel = connection.CreateModel())
            {
                ConfigureExchangesAndQueues(channel, configuration);
            }

        }

        public IEnumerable<ExchangeConfig> Exchanges()
        {
            return _settings.RabbitMQ.Exchanges.AsReadOnly();
        }

        public IEnumerable<QueueConfig> Queues()
        {
            return _settings.RabbitMQ.Queues.AsReadOnly();
        }

        public string GetRoutingKey(string queueName)
        {
            if (_queueRouteTable.TryGetValue(queueName, out var routingKey))
                return routingKey;

            return string.Empty;
        }

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _connection.Dispose();
                }
                _connection = null;
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
        }
    }
}
