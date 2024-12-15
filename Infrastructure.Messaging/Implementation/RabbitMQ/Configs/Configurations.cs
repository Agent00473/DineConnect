
using System.Xml.Serialization;

namespace Infrastructure.Messaging.Implementation.RabbitMQ.Configs
{

    public enum ExchangeCategory
    {
        None = 0,
        Pulse = 1,
        Integration
    }
    /// <summary>
    /// Queue configurations definition loaded from config file.
    /// </summary>
    [XmlRoot("QueueConfigurations")]
    public class QueueConfigurations
    {

        private string GetExchangeName(ExchangeCategory key)
        {
            var result =  RabbitMQ?.Exchanges?.FirstOrDefault(x => x.Category == key);
            return result?.Name ?? string.Empty;
        }

        public QueueConfigurations()
        {
            RabbitMQ = new RabbitMQSettings();
        }

        public ExchangeCategory GetExchangeCategory(string exchangeName)
        {
            var result = RabbitMQ?.Exchanges?.FirstOrDefault(x => x.Name == exchangeName);
            return result?.Category ?? ExchangeCategory.None;
        }

        [XmlElement("RabbitMQ")]
        public RabbitMQSettings RabbitMQ { get; set; }


        [XmlIgnore]
        public string IntegrationExchangeName => GetExchangeName(ExchangeCategory.Integration);

        [XmlIgnore]
        public string PulseExchangeName => GetExchangeName(ExchangeCategory.Pulse);
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
        public ExchangeConfig()
        {
            Name = string.Empty;
            Type = string.Empty;
            Category = ExchangeCategory.None;
        }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Type")]
        public string Type { get; set; }  // Type of the exchange (e.g., "direct", "topic", "fanout")

        [XmlElement("Durable")]
        public bool Durable { get; set; }

        [XmlElement(ElementName = "ExchangeCategory")]
        public int CategoryValue
        {
            get => (int)Category; 
            set => Category = (ExchangeCategory)value; 
        }

        [XmlIgnore] 
        public ExchangeCategory Category { get; set; }

    }

    public class QueueConfig
    {
        public QueueConfig()
        {
            Name = string.Empty;
            Exchange = string.Empty;
            RoutingKey = string.Empty;
        }

        [XmlElement("Name")]
        public string Name { get; set; } // Name of the queue

        [XmlElement("Exchange")]
        public string Exchange { get; set; }

        [XmlElement("RoutingKey")]
        public string RoutingKey { get; set; }

        [XmlElement("Durable")]
        public bool Durable { get; set; }

    }

}
