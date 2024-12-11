using Infrastructure.IntegrationEvents.Common;
using Infrastructure.IntegrationEvents.Database;
using Microsoft.EntityFrameworkCore;

namespace DineConnect.OrderManagementService.API
{
    public static class IntializeStartup
    {
        public static void Start(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                //Temporary Solution
                var context = scope.ServiceProvider.GetRequiredService<IntegrationEventDataContext>();
                var str =    context.Database.GetConnectionString();
                bool result = context.Database.EnsureCreated();
                if (result)
                {
                    Console.Write("Created...!!");
                }
                else
                {
                    Console.Write("Failure...!!");


                }

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
