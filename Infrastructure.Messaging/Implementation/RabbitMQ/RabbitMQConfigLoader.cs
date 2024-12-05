using Infrastructure.Messaging.Implementation.RabbitMQ.Configs;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Infrastructure.Messaging.Implementation.RabbitMQ
{

    /// <summary>
    /// Loads Queue configuration from config(.xml) file
    /// </summary>
    public class RabbitMQConfigLoader
    {
        public static QueueConfigurations LoadFromXml(string filePath)
        {
            // Load the XML file
            var doc = XDocument.Load(filePath);

            // Locate the QueueConfigurations element
            var queueConfigElement = doc.Descendants("QueueConfigurations").FirstOrDefault();
            if (queueConfigElement == null)
            {
                throw new InvalidDataException("QueueConfigurations tag not found in the XML file.");
            }

            // Deserialize the element
            var serializer = new XmlSerializer(typeof(QueueConfigurations));
            using var reader = new StringReader(queueConfigElement.ToString());
            return (QueueConfigurations)serializer.Deserialize(reader);
        }
    }

}
