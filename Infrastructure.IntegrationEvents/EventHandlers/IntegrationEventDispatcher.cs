using Infrastructure.IntegrationEvents.Database;
using Infrastructure.IntegrationEvents.Entities;
using Infrastructure.Messaging;
using Infrastructure.Messaging.Entities;
using Infrastructure.Messaging.Implementation.RabbitMQ;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.IntegrationEvents.EventHandlers
{
    /// <summary>
    /// Dispatches Integration Events to Queues Configured
    /// </summary>
    public class IntegrationEventDispatcher : IEventPublisher 
    {
        #region Private & Protected Fields
        private IIntegrationEventManagerService _eventManagerService;
        private IQueueMessagePublisher _messagePublisher;
        ///TODO: Make this a Seperate class Injected Refer RouteKeyManager
        private IDictionary<Type, string> _routedata = new Dictionary<Type, string>();

        #endregion

        #region Protected & Private Methods
        private string GetRouteKey(Type type)
        {
            return _routedata[type];
        }

        private async Task<bool> PublishEvents(IEnumerable<IntegrationEventDetail> pendingLogEvents)
        {
            bool allEventsProcessed = true;

            foreach (var logEvt in pendingLogEvents)
            {
                bool eventProcessed = false;

                try
                {
                    // Retry logic
                    for (int attempt = 1; attempt <= 3; attempt++)
                    {
                        if (ProcessEvent(logEvt))
                        {
                            eventProcessed = true;
                            break; // Exit retry loop on success
                        }

                        if (attempt == 3)
                        {
                            // Mark as failed after maximum retries
                            _eventManagerService.MarkEventAsFailed(logEvt.EventId);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing event {logEvt.EventId}: {ex.Message}");
                    // Mark the event as failed in case of exception
                    _eventManagerService.MarkEventAsFailed(logEvt.EventId);
                }

                // Save changes after processing each event
                try
                {
                    await _eventManagerService.SaveChagesAsync();
                }
                catch (Exception saveEx)
                {
                    Console.WriteLine($"Error saving changes for event {logEvt.EventId}: {saveEx.Message}");
                    return false; // Fail fast if saving changes fails
                }

                // Update the overall status
                if (!eventProcessed)
                {
                    allEventsProcessed = false;
                }
            }

            return allEventsProcessed;
        }

        private bool ProcessEvent(IntegrationEventDetail logEvt)
        {
            _eventManagerService.MarkEventAsInProgress(logEvt.EventId);
            var obj = logEvt.IntegrationEvent;
            var key = GetRouteKey(obj.GetType());
            var msg = obj;
            if (_messagePublisher.SendMessage(key, msg))
            {
                _eventManagerService.MarkEventAsPublished(logEvt.EventId);
                return true;
            }
            return false;
        }
        #endregion

        #region Constructors
        private  IntegrationEventDispatcher(IConfiguration configuration, IQueueMessagePublisher messagePublisher)
        {
            _eventManagerService = new IntegrationEventManagerService(configuration);
            _messagePublisher = messagePublisher;
        }

        /// <summary>
        /// Only for Test Application
        /// </summary>
        /// <param name="context"></param>
        /// <param name="messagePublisher"></param>
        private IntegrationEventDispatcher(IntegrationEventDataContext context, IQueueMessagePublisher messagePublisher)
        {
            _eventManagerService = new IntegrationEventManagerService(context);
            _messagePublisher = messagePublisher;
        }

        #endregion

        #region Public Properties
        // Add public properties here
        #endregion

        #region Public Methods

        internal void AddRouteData(Type key, string value) {
            _routedata[key]= value;
        }
        public async Task<bool> Publish<TData>(Guid transactionId) where TData : IntegrationEvent
        {
            var pendingLogEvents = await _eventManagerService.RetrievePendingEventLogsToPublishAsync(transactionId);
            return await PublishEvents(pendingLogEvents);
        }

        public async Task<bool> PublishAll<TData>() where TData : IntegrationEvent
        {
            var pendingLogEvents = await _eventManagerService.RetrieveAllPendingEventLogsToPublishAsync();
            return await PublishEvents(pendingLogEvents);
        }

        public Task<bool> AddPulse<TData>() where TData : EventMessage
        {
            return _eventManagerService.AddHeartBeatAsync();
        }
        #endregion

        #region Factory Methods
        public static IntegrationEventDispatcher Create(IntegrationEventDataContext context, IQueueMessagePublisher messagePublisher, IRabbitMQConfigurationManager configurationManager)
        {
            var dispatcher =  new IntegrationEventDispatcher(context, messagePublisher);
            //var key=configurationManager.GetRoutingKey("SampleQueue");
            //dispatcher.AddRouteData(typeof(string), key);
            var key = configurationManager.GetRoutingKey("CustomerQueue");
            dispatcher.AddRouteData(typeof(CustomerEvent), key);

            key = configurationManager.GetRoutingKey("OrderQueue");
            dispatcher.AddRouteData(typeof(OrderEvent), key);
            key = configurationManager.GetRoutingKey("HeartBeatQueue");
            dispatcher.AddRouteData(typeof(HeartBeatEvent), key);

            
            return dispatcher;
        }

       
        #endregion
    }
}
