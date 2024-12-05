
namespace Infrastructure.Messaging.Common
{
    /// <summary>
    /// Class maintains the Routing Key for each Type, this can be static defined
    /// IRabbitMQConfigurationManager has the route data. Refer IntegrationEventDispatcher Factory Method for sample approach
    /// </summary>
    internal static class RouteKeyManager
    {
        
        
         public static string GetRouteData(Type type)
        {
            //var key = configurationManager.GetRoutingKey("SampleQueue");
            //dispatcher.AddRouteData(typeof(string), key);
            //key = configurationManager.GetRoutingKey("CustomerQueue");
            //dispatcher.AddRouteData(typeof(CustomerEvent), key);

            return string.Empty;
        }
        
    }
}
