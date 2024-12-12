using Infrastructure.Messaging.Implementation.RabbitMQ.Configs;
using Microsoft.Extensions.Configuration;


namespace Infrastructure.IntegrationEvents.Common
{
    public static class MessageBrokerConfigLoader
    {
        private static IConfigurationRoot _configuration;
        static MessageBrokerConfigLoader()
        {
            var path = Directory.GetCurrentDirectory();
            string currentDirectory = Environment.CurrentDirectory;
            string executionDirectory = Path.Combine(AppContext.BaseDirectory, "configs");

            var builder = new ConfigurationBuilder()
                .SetBasePath(executionDirectory)
                .AddJsonFile("infrasettings.json", optional: true, reloadOnChange: true);
            _configuration = builder.Build();


        }
        public static string GetSetting(string key)
        {
            return _configuration[key] ?? string.Empty;
        }

        public static QueueConfigurations? GetQueueConfiguration()
        {
            var queueConfig = _configuration.GetSection("QueueConfigurations").Get<QueueConfigurations>();
            return queueConfig;
        }
    }
}
