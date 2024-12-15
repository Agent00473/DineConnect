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
        private readonly string _connectionString;

        ///TODO: Make this a Seperate class Injected Refer RouteKeyManager
        private IDictionary<Type, RouteData> _routedata = new Dictionary<Type, RouteData>();
        #endregion

        #region Protected & Private Methods
        private RouteData GetRouteData(Type type)
        {
            return _routedata[type];
        }

        private async Task<bool> PublishEvents(IEnumerable<IntegrationEventDetail> pendingLogEvents)
        {
            bool allEventsProcessed = true;
            if (pendingLogEvents.Any())
            {
                using (var handler = new PublishIntegrationEventCommandHandler(_connectionString))
                {
                    foreach (var logEvt in pendingLogEvents)
                    {
                        bool eventProcessed = false;

                        try
                        {
                            // Retry logic
                            for (int attempt = 1; attempt <= 3; attempt++)
                            {
                                if (ProcessEvent(handler, logEvt))
                                {
                                    eventProcessed = true;
                                    break; // Exit retry loop on success
                                }

                                if (attempt == 3)
                                {
                                    // Mark as failed after maximum retries
                                    handler.MarkEventAsFailed(logEvt.EventId);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error processing event {logEvt.EventId}: {ex.Message}");
                            // Mark the event as failed in case of exception
                            handler.MarkEventAsFailed(logEvt.EventId);
                        }

                        // Save changes after processing each event
                        try
                        {
                            await handler.SaveChagesAsync();
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
                }
            }
            return allEventsProcessed;
        }

        private bool ProcessEvent(IPublishIntegrationEventCommandHandler handler, IntegrationEventDetail logEvt)
        {
            handler.MarkEventAsInProgress(logEvt.EventId);
            var obj = logEvt.IntegrationEvent;
            var rData = GetRouteData(obj.GetType());
            var msg = obj;
            if (_messagePublisher.SendMessage(rData, msg))
            {
                return handler.MarkEventAsPublished(logEvt.EventId);
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
        }

        private IntegrationEventPublisher(IIntegrationEventsQueryHandler queryIntegrationEvents,
       IAddIntegrationEventCommandHandler eventsAddCommandHandler,
       IMessagePublisher messagePublisher, string connectionString)
        {
            _queryIntegrationEvents = queryIntegrationEvents;
            _messagePublisher = messagePublisher;
            _eventsAddCommandHandler = eventsAddCommandHandler;
            _connectionString = connectionString;
        }
        #endregion

        #region Public Properties
        // Add public properties here
        #endregion

        #region Public Methods
        internal void AddRouteData(Type key, RouteData value)
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

        public async Task<bool> AddPulse<TData>() where TData : EventMessage
        {
            return await _eventsAddCommandHandler.AddHeartBeatEventDataAsync();
        }
        #endregion

        #region Factory Methods
        //public static IntegrationEventPublisher Create(string connectionString, IMessagePublisher messagePublisher, IRabbitMQConfigurationManager configurationManager)
        //{
        //    var qryHandler = IntegrationEventsQueryHandler.Create(connectionString);
        //    var addHandler = AddIntegrationEventCommandHandler.Create(connectionString);
        //    var dispatcher = new IntegrationEventPublisher(qryHandler, addHandler, messagePublisher);

        //    var rData = configurationManager.GetRoutingData("CustomerQueue");
        //    dispatcher.AddRouteData(typeof(CustomerIntegrationEvent), rData);

        //    rData = configurationManager.GetRoutingData("OrderQueue");
        //    dispatcher.AddRouteData(typeof(OrderEvent), rData);
        //    rData = configurationManager.GetRoutingData("HeartBeatQueue");
        //    dispatcher.AddRouteData(typeof(HeartBeatEvent), rData);
        //    return dispatcher;
        //}

        public static IntegrationEventPublisher Create(IIntegrationEventsQueryHandler qryHandler, IAddIntegrationEventCommandHandler addCmdHandler, 
           IMessagePublisher messagePublisher, IRabbitMQConfigurationManager configurationManager, string connectionString)
        {
            Console.WriteLine("###### IntegrationEventPublisher Created ...!! ######");
            var dispatcher = new IntegrationEventPublisher(qryHandler, addCmdHandler, messagePublisher, connectionString);

            var rData = configurationManager.GetRoutingData("CustomerQueue");
            dispatcher.AddRouteData(typeof(CustomerIntegrationEvent), rData);

            rData = configurationManager.GetRoutingData("OrderQueue");
            dispatcher.AddRouteData(typeof(OrderEvent), rData);
            rData = configurationManager.GetRoutingData("HeartBeatQueue");
            dispatcher.AddRouteData(typeof(HeartBeatEvent), rData);


            return dispatcher;
        }
        #endregion
    }
}
