using Infrastructure.IntegrationEvents.DataAccess.Commands;
using Infrastructure.IntegrationEvents.DataAccess.Queries;
using Infrastructure.IntegrationEvents.Entities;
using Infrastructure.IntegrationEvents.Entities.Events;
using Infrastructure.Messaging;
using Infrastructure.Messaging.Entities;
using Infrastructure.Messaging.Implementation.RabbitMQ;

namespace Infrastructure.IntegrationEvents.EventHandlers.Implementations
{
    /// <summary>
    /// Publishes Integration Events to Queues Configured
    /// </summary>
    public class IntegrationEventPublisher : IEventPublisher
    {
        #region Private & Protected Fields
        private IMessagePublisher _messagePublisher;
        private readonly IIntegrationEventsQueryHandler _queryIntegrationEvents;
        private readonly IAddIntegrationEventCommandHandler _eventsAddCommandHandler;
        private readonly IPublishIntegrationEventCommandHandler _handler;
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
                        if (ProcessEvent(_handler, logEvt))
                        {
                            eventProcessed = true;
                            break; // Exit retry loop on success
                        }

                        if (attempt == 3)
                        {
                            // Mark as failed after maximum retries
                            _handler.MarkEventAsFailed(logEvt.EventId);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing event {logEvt.EventId}: {ex.Message}");
                    // Mark the event as failed in case of exception
                    _handler.MarkEventAsFailed(logEvt.EventId);
                }

                // Save changes after processing each event
                try
                {
                    await _handler.SaveChagesAsync();
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

        private bool ProcessEvent(IPublishIntegrationEventCommandHandler handler, IntegrationEventDetail logEvt)
        {
            handler.MarkEventAsInProgress(logEvt.EventId);
            var obj = logEvt.IntegrationEvent;
            var key = GetRouteKey(obj.GetType());
            var msg = obj;
            if (_messagePublisher.SendMessage(key, msg))
            {
                handler.MarkEventAsPublished(logEvt.EventId);
                return true;
            }
            return false;
        }
        #endregion

        #region Constructors
        private IntegrationEventPublisher(IIntegrationEventsQueryHandler queryIntegrationEvents,
            IAddIntegrationEventCommandHandler eventsAddCommandHandler,
            IPublishIntegrationEventCommandHandler handler, IMessagePublisher messagePublisher)
        {
            _queryIntegrationEvents = queryIntegrationEvents;
            _messagePublisher = messagePublisher;
            _eventsAddCommandHandler = eventsAddCommandHandler;
            _handler = handler;
        }

        #endregion

        #region Public Properties
        // Add public properties here
        #endregion

        #region Public Methods

        internal void AddRouteData(Type key, string value)
        {
            _routedata[key] = value;
        }
        public async Task<bool> Publish<TData>(Guid transactionId) where TData : IntegrationEvent
        {
            var pendingLogEvents = await _queryIntegrationEvents.RetrievePendingEventLogsToPublishAsync(transactionId);
            return await PublishEvents(pendingLogEvents);
        }

        public async Task<bool> PublishAll<TData>() where TData : IntegrationEvent
        {
            var pendingLogEvents = await _queryIntegrationEvents.RetrieveAllPendingEventLogsToPublishAsync();
            return await PublishEvents(pendingLogEvents);
        }

        public Task<bool> AddPulse<TData>() where TData : EventMessage
        {
            return _eventsAddCommandHandler.AddHeartBeatEventDataAsync();
        }
        #endregion

        #region Factory Methods
        //public static IntegrationEventPublisher Create(string connectionString, IMessagePublisher messagePublisher, IRabbitMQConfigurationManager configurationManager)
        //{
        //    var qryHandler = IntegrationEventsQueryHandler.Create(connectionString);
        //    var addHandler = AddIntegrationEventCommandHandler.Create(connectionString);
        //    var dispatcher = new IntegrationEventPublisher(qryHandler, addHandler, messagePublisher);

        //    var key = configurationManager.GetRoutingKey("CustomerQueue");
        //    dispatcher.AddRouteData(typeof(CustomerIntegrationEvent), key);

        //    key = configurationManager.GetRoutingKey("OrderQueue");
        //    dispatcher.AddRouteData(typeof(OrderEvent), key);
        //    key = configurationManager.GetRoutingKey("HeartBeatQueue");
        //    dispatcher.AddRouteData(typeof(HeartBeatEvent), key);
        //    return dispatcher;
        //}

        public static IntegrationEventPublisher Create(IIntegrationEventsQueryHandler qryHandler, IAddIntegrationEventCommandHandler addCmdHandler, IPublishIntegrationEventCommandHandler pubHandler, IMessagePublisher messagePublisher, IRabbitMQConfigurationManager configurationManager)
        {
            var dispatcher = new IntegrationEventPublisher(qryHandler, addCmdHandler, pubHandler, messagePublisher);

            var key = configurationManager.GetRoutingKey("CustomerQueue");
            dispatcher.AddRouteData(typeof(CustomerIntegrationEvent), key);

            key = configurationManager.GetRoutingKey("OrderQueue");
            dispatcher.AddRouteData(typeof(OrderEvent), key);
            key = configurationManager.GetRoutingKey("HeartBeatQueue");
            dispatcher.AddRouteData(typeof(HeartBeatEvent), key);


            return dispatcher;
        }


        #endregion
    }
}
