using DineConnect.OrderManagementService.Domain.Customers;
using DineConnect.OrderManagementService.Domain.Orders.Entities;
using DineConnect.OrderManagementService.Domain.Orders;
using DineConnect.OrderManagementService.Infrastructure.DataAccess.Repositories.Implementation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DineConnect.OrderManagementService.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using DineConnect.OrderManagementService.Application.Interfaces;
using DineConnect.OrderManagementService.Infrastructure.ExceptionHandlers;
using DineConnect.OrderManagementService.Infrastructure.EventHandlers;
using Infrastructure.IntegrationEvents.Common.Configs;
using Infrastructure.IntegrationEvents.Common;
using Infrastructure.Messaging.Implementation.RabbitMQ;

namespace DineConnect.OrderManagementService.Infrastructure
{
    public static class DependencyRegistration
    {
        public static IServiceCollection AddOrderManagementInfrastructureDependencies(this IServiceCollection services,
                                    IConfiguration configration)
        {
            services.AddPersistance(configration)
                    .AddExceptionHandler(configration);
            return services;
        }

        private static IServiceCollection AddExceptionHandler(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddExceptionHandler<CreateCustomerExceptionHandler>();
            return services;
        }
        private static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DineOutOrderDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IDomainEventPublisher, DomainEventPublisher>();
           
            services.AddScoped<IRepository<Order>, OrderRepository>();
            services.AddScoped<IRepository<OrderItem>, MenuItemRepository>();
            services.AddScoped<IRepository<Customer>, CustomerRepository>();
            services.AddScoped<IRepository<Restaurant>, RestaurantRepository>();

            
            return services;
        }


        private static IServiceCollection AddIntegrationEventHandlers(this IServiceCollection services, IConfiguration configuration, IServiceProvider provider)
        {
            services.AddExceptionHandler<CreateCustomerExceptionHandler>();
            var conString = configuration.GetConnectionString("DefaultConnection");
            AddMessageQueue(services, conString);
            return services;
        }


        private static IServiceCollection AddMessageQueue(IServiceCollection services, string connectionString)
        {
            var data = IntegrationConfiguration.GetQueueConfiguration();
            var rabbitMQConfigurationManager = new RabbitMQConfigurationManager(data);
            rabbitMQConfigurationManager.Initialize();
            services.AddSingleton<IRabbitMQConfigurationManager>(rabbitMQConfigurationManager);
            services.AddSingleton<IIntegrationEventDataDispatcher>(sp =>
            {
                var qConfig = sp.GetRequiredService<IRabbitMQConfigurationManager>();
                return IntegrationEventDataDispatcher.Create(connectionString, data.IntegrationExchangeName, qConfig);
            });
            return services;
        }
    }
}
