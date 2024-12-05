
using System.Xml.Serialization;

namespace Infrastructure.Messaging.Implementation.RabbitMQ.Configs
{

    /// <summary>
    /// Queue configurations definition loaded from config file.
    /// </summary>
    [XmlRoot("QueueConfigurations")]
    public class QueueConfigurations
    {
        [XmlElement("RabbitMQ")]
        public RabbitMQSettings RabbitMQ { get; set; }

        public QueueConfigurations()
        {
            RabbitMQ = new RabbitMQSettings();
        }
    }


    public class RabbitMQSettings
    {
        [XmlElement("HostName")]
        public string HostName { get; set; } = string.Empty;

        [XmlElement("Port")]
        public int Port { get; set; }

        [XmlArray("Exchanges")]
        [XmlArrayItem("Exchange")]
        public List<ExchangeConfig> Exchanges { get; set; } = new();

        [XmlArray("Queues")]
        [XmlArrayItem("Queue")]
        public List<QueueConfig> Queues { get; set; } = new();
    }

    public class ExchangeConfig
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Type")]
        public string Type { get; set; }  // Type of the exchange (e.g., "direct", "topic", "fanout")

        [XmlElement("Durable")]
        public bool Durable { get; set; }

        public ExchangeConfig()
        {
            Name = string.Empty;
            Type = string.Empty;
        }
    }

    public class QueueConfig
    {
        [XmlElement("Name")]
        public string Name { get; set; } // Name of the queue

        [XmlElement("Exchange")]
        public string Exchange { get; set; }

        [XmlElement("RoutingKey")]
        public string RoutingKey { get; set; }

        [XmlElement("Durable")]
        public bool Durable { get; set; }

        public QueueConfig()
        {
            Name = string.Empty;
            Exchange = string.Empty;
            RoutingKey = string.Empty;
        }
    }

}
