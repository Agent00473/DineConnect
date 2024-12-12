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
                AddMessageQueue(connectionStringKey).
                AddCreateIntegrationEventHandler(connectionStringKey).
                AddIntegrationEventDispatcher();
            return services;
        }

        private static IServiceCollection AddCreateIntegrationEventHandler(this IServiceCollection services, string connectionStringKey)
        {
            //services.AddScoped<IAddIntegrationEventCommandHandler>(sp =>
            //{
            //    var configuration = sp.GetRequiredService<IConfiguration>();
            //    var connectionString = configuration.GetConnectionString(connectionStringKey);
            //    return AddIntegrationEventCommandHandler.Create(connectionString);
            //});
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

        private static IServiceCollection AddMessageQueue(this IServiceCollection services, string connectionStringKey)
        {
            var data = MessageBrokerConfigLoader.GetQueueConfiguration();
            var rabbitMQConfigurationManager = new RabbitMQConfigurationManager(data);
            rabbitMQConfigurationManager.Initialize();
            services.AddSingleton<IRabbitMQConfigurationManager>(rabbitMQConfigurationManager);

            services.AddTransient<IIntegrationEventsQueryHandler>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                return IntegrationEventsQueryHandler.Create(config);
            });

            services.AddTransient<IAddIntegrationEventCommandHandler>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                return AddIntegrationEventCommandHandler.Create(config);
            });

            services.AddTransient<IMessagePublisher>(sp =>
            {
                var config = sp.GetRequiredService<IRabbitMQConfigurationManager>();
                return RabbitMQueuePublisher.Create(config.IntegrationExchangeName, config);
            });

            services.AddTransient<IPublishIntegrationEventCommandHandler>(sp =>
            {
                using (var scope = sp.CreateScope()) // Create a scope to resolve scoped services
                {
                    var ctxt = scope.ServiceProvider.GetRequiredService<IntegrationEventDataContext>();
                    return PublishIntegrationEventCommandHandler.Create(ctxt);
                }
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
            return services;
        }

        private static IServiceCollection AddIntegrationEventDispatcher(this IServiceCollection services)//, string connectionStringKey)
        {
            services.AddSingleton<IIntegrationEventDataDispatcher>(sp =>
            {
                var publisher = sp.GetRequiredService<IEventPublisher>();
                return IntegrationEventDataDispatcher.Create(publisher);
            });
            return services;

            //services.AddSingleton<IIntegrationEventDataDispatcher>(sp =>
            //{
            //    var configuration = sp.GetRequiredService<IConfiguration>();
            //    var connectionString = configuration.GetConnectionString(connectionStringKey);
            //    var qConfig = sp.GetRequiredService<IRabbitMQConfigurationManager>();

            //    return IntegrationEventDataDispatcher.Create(connectionString, qConfig.IntegrationExchangeName, qConfig);
            //});
            //return services;
        }

    }
}
