using Infrastructure.Messaging.Implementation.RabbitMQ.Configs;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Infrastructure.Messaging.Implementation.RabbitMQ
{

    public class RabbitMQConfigLoader
    {
        //public static QueueConfigurations LoadFromXml(string filePath)
        //{
        //    // Load the XML document
        //    var doc = new XmlDocument();
        //    doc.Load(filePath);

        //    // Locate the QueueConfigurations node
        //    var node = doc.SelectSingleNode("//QueueConfigurations");
        //    if (node == null)
        //    {
        //        throw new InvalidDataException("QueueConfigurations tag not found in the XML file.");
        //    }

        //    var serializer = new XmlSerializer(typeof(QueueConfigurations));
        //    using var stream = new FileStream(filePath, FileMode.Open);
        //    return (QueueConfigurations)serializer.Deserialize(stream);
        //}

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
