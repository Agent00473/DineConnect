using Infrastructure.IntegrationEvents.Common;
using Infrastructure.IntegrationEvents.DataAccess;
using Infrastructure.IntegrationEvents.DataAccess.Commands;
using Infrastructure.IntegrationEvents.DataAccess.Queries;
using Infrastructure.IntegrationEvents.EventHandlers;
using Infrastructure.IntegrationEvents.EventHandlers.Implementations;
using Infrastructure.Messaging;
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
            services.AddPersistance(connectionStringKey).
                AddMessageQueueConfiguration(connectionStringKey).
                AddIntegrationEventHandlers(connectionStringKey).
                AddIntegrationEventDispatcher();
            return services;
        }

        private static IServiceCollection AddIntegrationEventHandlers(this IServiceCollection services, string connectionStringKey)
        {
            services.AddTransient<IAddIntegrationEventCommandHandler>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                return AddIntegrationEventCommandHandler.Create(config);
            });

            services.AddTransient<IIntegrationEventsQueryHandler>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                return IntegrationEventsQueryHandler.Create(config);
            });

            services.AddTransient<IPublishIntegrationEventCommandHandler>(sp =>
            {
                //Created a Scopefactory to keep the DataContext alive through out the lifetime of IntegrationEventCommandHandler
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                var scope = scopeFactory.CreateScope();
                var ctxt = scope.ServiceProvider.GetRequiredService<IntegrationEventDataContext>();
                return PublishIntegrationEventCommandHandler.Create(ctxt);
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
            });

            return services;
        }

        private static IServiceCollection AddMessageQueueConfiguration(this IServiceCollection services, string connectionStringKey)
        {
            services.AddSingleton<IRabbitMQConfigurationManager>((sp) =>
            {
                var data = MessageBrokerConfigLoader.GetQueueConfiguration();
                var rabbitMQConfigurationManager = new RabbitMQConfigurationManager(data);
                //rabbitMQConfigurationManager.Initialize();
                return rabbitMQConfigurationManager;
            });

            return services;
        }

        private static IServiceCollection AddIntegrationEventDispatcher(this IServiceCollection services)//, string connectionStringKey)
        {

            services.AddTransient<IMessagePublisher>(sp =>
            {
                var config = sp.GetRequiredService<IRabbitMQConfigurationManager>();
                return RabbitMQueuePublisher.Create(config.IntegrationExchangeName, config);
            });

            services.AddTransient<IEventPublisher>(sp =>
            {
                var config = sp.GetRequiredService<IRabbitMQConfigurationManager>();
                var qryHandler = sp.GetRequiredService<IIntegrationEventsQueryHandler>();
                var addCmdHandler = sp.GetRequiredService<IAddIntegrationEventCommandHandler>();
                var messagePublisher = sp.GetRequiredService<IMessagePublisher>();
                var pubHandler = sp.GetRequiredService<IPublishIntegrationEventCommandHandler>();
                return IntegrationEventPublisher.Create(qryHandler, addCmdHandler, pubHandler, messagePublisher, config);
            });

            services.AddSingleton<IIntegrationEventDataDispatcher>(sp =>
            {
                var publisher = sp.GetRequiredService<IEventPublisher>();
                return IntegrationEventDataDispatcher.Create(publisher);
            });
            return services;
        }

    }
}
