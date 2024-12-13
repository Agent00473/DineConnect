using Infrastructure.IntegrationEvents;
using Infrastructure.IntegrationEvents.DataAccess;
using Infrastructure.Messaging.Implementation.RabbitMQ;
using Microsoft.EntityFrameworkCore;

namespace DineConnect.OrderManagementService.API
{
    public static class IntializeStartup
    {
        public static void Start(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var qManager = scope.ServiceProvider.GetRequiredService<IRabbitMQConfigurationManager>();
                qManager.Initialize();

                var startupService = scope.ServiceProvider.GetRequiredService<IIntegrationEventDataDispatcher>();
                startupService.Start(); // Call a custom start method
            }
        }

        public static void Shutdown(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IIntegrationEventDataDispatcher>();
                service.Stop(); // Call a custom start method
            }
        }
    }
}
