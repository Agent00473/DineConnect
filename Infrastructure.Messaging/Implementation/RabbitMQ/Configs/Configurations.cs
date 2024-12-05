
using System.Xml.Serialization;

namespace Infrastructure.Messaging.Implementation.RabbitMQ.Configs
{

    /// <summary>
    /// Queue configurations definition loaded from config file.
    /// </summary>
    [XmlRoot("QueueConfigurations")]
    public class QueueConfigurations
    {
        public RabbitMQSettings RabbitMQ { get; set; }

        public QueueConfigurations()
        {
            RabbitMQ = new RabbitMQSettings();
        }
    }

    public class RabbitMQSettings
    {
        public string HostName { get; set; } = String.Empty;
        public int Port { get; set; }
        public List<ExchangeConfig> Exchanges { get; set; } = new();
        public List<QueueConfig> Queues { get; set; } = new();
    }

    public class ExchangeConfig
    {
        public string Name { get; set; }
        public string Type { get; set; }  // Type of the exchange (e.g., "direct", "topic", "fanout")
        public bool Durable { get; set; }
        public ExchangeConfig()
        {
            Name = string.Empty;
            Type = string.Empty;
        }
    }

    public class QueueConfig
    {
        public string Name { get; set; } // Name of the queue
        public string Exchange { get; set; } 
        public string RoutingKey { get; set; }
        public bool Durable { get; set; }
        public QueueConfig()
        {
            Name = string.Empty;
            Exchange = string.Empty;
            RoutingKey = string.Empty;
        }
    }
}
