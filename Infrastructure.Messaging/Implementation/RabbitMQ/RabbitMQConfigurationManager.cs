using global::RabbitMQ.Client;
using Infrastructure.Messaging.Common;
using Infrastructure.Messaging.Implementation.RabbitMQ.Configs;
using Microsoft.Extensions.Options;

namespace Infrastructure.Messaging.Implementation.RabbitMQ
{
    public record RouteData(string ExchangeName, string RouteKey, ExchangeCategory Category)
    {
        public RouteData() : this(string.Empty, string.Empty, ExchangeCategory.None)
        {
        }
    };


    public interface IRabbitMQConfigurationManager : IDisposable
    {
        void AddQueue(QueueConfiguration configuration);
        IEnumerable<ExchangeConfig> Exchanges();
        IEnumerable<QueueConfig> Queues();
        RouteData GetRoutingData(string queueName);
        void Initialize();
        public IConnection GetConnection();
        public string IntegrationExchangeName { get; }
        public string PulseExchangeName { get; }

    }

    /// <summary>
    /// Manager class responsible for configuring Queues, Exchanges, and Bindings based on the defined Queue Configuration settings.
    /// </summary>
    public sealed class RabbitMQConfigurationManager : IRabbitMQConfigurationManager
    {
        private readonly QueueConfigurations _qConfigs;
        private bool disposedValue;
        private IConnection _connection;
        private IDictionary<string, RouteData> _queueRouteTable = new Dictionary<string, RouteData>();

        public string IntegrationExchangeName => _qConfigs.IntegrationExchangeName;
        public string PulseExchangeName => _qConfigs.PulseExchangeName;

        public RabbitMQConfigurationManager(IOptions<QueueConfigurations> options)
        {
            _qConfigs = options.Value;
        }

        public RabbitMQConfigurationManager(QueueConfigurations config)
        {
            _qConfigs = config;
        }

        private void ConfigureExchangesAndQueues(IModel channel)
        {
            foreach (var exchange in _qConfigs.RabbitMQ.Exchanges)
            {
                channel.ExchangeDeclare(exchange.Name, exchange.Type, exchange.Durable);
            }

            foreach (var queue in _qConfigs.RabbitMQ.Queues)
            {
                ConfigureQueue(channel, queue);
            }
        }

        private ExchangeCategory GetExchangeCategory(string exchangeName)
        {
            return _qConfigs.GetExchangeCategory(exchangeName);
        }

        private void ConfigureQueue(IModel channel, QueueConfig config)
        {
            channel.QueueDeclare(config.Name, config.Durable, false, false, null);
            channel.QueueBind(config.Name, config.Exchange, config.RoutingKey);
            _queueRouteTable[config.Name] = new RouteData(config.Exchange,config.RoutingKey, GetExchangeCategory(config.Exchange));
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
                var factory = QueueConnectionFactory.GetFactory(_qConfigs.RabbitMQ);
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
            return _qConfigs.RabbitMQ.Exchanges.AsReadOnly();
        }

        public IEnumerable<QueueConfig> Queues()
        {
            return _qConfigs.RabbitMQ.Queues.AsReadOnly();
        }

        public RouteData GetRoutingData(string queueName)
        {
            if (_queueRouteTable.TryGetValue(queueName, out var routingKey))
                return routingKey;

            return new RouteData();
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
