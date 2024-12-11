using Infrastructure.IntegrationEvents.Common;
using Infrastructure.IntegrationEvents.Common.Configs;
using Infrastructure.IntegrationEvents.Database;
using Infrastructure.IntegrationEvents.Database.Commands;
using Infrastructure.Messaging.Implementation.RabbitMQ;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IntegrationEvents
{
    public static class DependencyRegistration
    {
        public static IServiceCollection AddServiceBlockInfrastructure(this IServiceCollection services,
                                 string connectionStringKey = "DefaultConnection")
        {
            services.AddMessageQueue(connectionStringKey).
                AddPersistance(connectionStringKey).
                AddCreateIntegrationEventHandler(connectionStringKey).
                AddIntegrationEventDispatcher(connectionStringKey);
            return services;
        }

        private static IServiceCollection AddCreateIntegrationEventHandler(this IServiceCollection services, string connectionStringKey)
        {
            services.AddScoped<IAddIntegrationEventCommandHandler>(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString(connectionStringKey);
                return AddIntegrationEventCommandHandler.Create(connectionString);
            });
            return services;
        }

        private static IServiceCollection AddPersistance(this IServiceCollection services, string connectionStringKey)
        {
            services.AddDbContext<IntegrationEventDataContext>((provider, options) =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString(connectionStringKey);
                options.UseNpgsql(connectionString);
            }, ServiceLifetime.Scoped);
            return services;
        }

        private static IServiceCollection AddMessageQueue(this IServiceCollection services, string connectionStringKey)
        {
            var data = IntegrationConfiguration.GetQueueConfiguration();
            var rabbitMQConfigurationManager = new RabbitMQConfigurationManager(data);
            rabbitMQConfigurationManager.Initialize();
            services.AddSingleton<IRabbitMQConfigurationManager>(rabbitMQConfigurationManager);
            services.AddSingleton<IIntegrationEventDataDispatcher>(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString(connectionStringKey);
                var qConfig = sp.GetRequiredService<IRabbitMQConfigurationManager>();
                EnsureCreation(sp);

                return IntegrationEventDataDispatcher.Create(connectionString, data.IntegrationExchangeName, qConfig);
            });
            return services;
        }

        private static IServiceCollection AddIntegrationEventDispatcher(this IServiceCollection services, string connectionStringKey)
        {
            services.AddSingleton<IIntegrationEventDataDispatcher>(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString(connectionStringKey);
                var qConfig = sp.GetRequiredService<IRabbitMQConfigurationManager>();

                return IntegrationEventDataDispatcher.Create(connectionString, qConfig.IntegrationExchangeName, qConfig);
            });
            return services;
        }

        private static void EnsureCreation(IServiceProvider sp)
        {
           
        }
    }
}
