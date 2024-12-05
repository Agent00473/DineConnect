using Infrastructure.IntegrationEvents.Database;
using Infrastructure.IntegrationEvents.Entities;
using Infrastructure.Messaging;
using Infrastructure.Messaging.Implementation.RabbitMQ;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.IntegrationEvents.EventHandlers
{
    public class IntegrationEventDispatcher : IEventPublisher 
    {
        #region Private & Protected Fields
        private IIntegrationEventManagerService _eventManagerService;
        private IMessagePublisher _messagePublisher;
        private IDictionary<Type, string> _routedata = new Dictionary<Type, string>();

        #endregion

        #region Protected & Private Methods
        private string GetRouteKey(Type type)
        {
            return _routedata[type];
        }
        #endregion

        #region Constructors
        private  IntegrationEventDispatcher(IConfiguration configuration, IMessagePublisher messagePublisher)
        {
            _eventManagerService = new IntegrationEventManagerService(configuration);
            _messagePublisher = messagePublisher;
        }

        /// <summary>
        /// Only for Test Application
        /// </summary>
        /// <param name="context"></param>
        /// <param name="messagePublisher"></param>
        private IntegrationEventDispatcher(IntegrationEventDataContext context, IMessagePublisher messagePublisher)
        {
            _eventManagerService = new IntegrationEventManagerService(context);
            _messagePublisher = messagePublisher;
        }

        #endregion

        #region Public Properties
        // Add public properties here
        #endregion

        #region Public Methods

        private bool ProcessEvent<TData>(IntegrationEventDetail logEvt) where TData : IntegrationEvent
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

        internal void AddRouteData(Type key, string value) {
            _routedata[key]= value;
        }
        public async Task<bool> Publish<TData>(Guid transactionId) where TData : IntegrationEvent
        {
            var pendingLogEvents = await _eventManagerService.RetrievePendingEventLogsToPublishAsync(transactionId);
            bool allEventsProcessed = true;

            foreach (var logEvt in pendingLogEvents)
            {
                bool eventProcessed = false;

                try
                {
                    // Retry logic
                    for (int attempt = 1; attempt <= 3; attempt++)
                    {
                        if (ProcessEvent<TData>(logEvt))
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

        #endregion
         
        #region Factory Methods
        public static IntegrationEventDispatcher Create(IntegrationEventDataContext context, IMessagePublisher messagePublisher, IRabbitMQConfigurationManager configurationManager)
        {
            var dispatcher =  new IntegrationEventDispatcher(context, messagePublisher);
            var key=configurationManager.GetRoutingKey("SampleQueue");
            dispatcher.AddRouteData(typeof(string), key);
            key = configurationManager.GetRoutingKey("CustomerQueue");
            dispatcher.AddRouteData(typeof(CustomerEvent), key);

            return dispatcher;
        }
        #endregion
    }
}
