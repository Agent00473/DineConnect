using Infrastructure.IntegrationEvents.Database;
using Infrastructure.IntegrationEvents.Database.Commands;
using Infrastructure.IntegrationEvents.Database.Queries;
using Infrastructure.IntegrationEvents.Events;
using Infrastructure.Messaging;
using Infrastructure.Messaging.Entities;
using Infrastructure.Messaging.Implementation.RabbitMQ;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.IntegrationEvents.EventHandlers
{
    /// <summary>
    /// Dispatches Integration Events to Queues Configured
    /// </summary>
    public class IntegrationEventDispatcher : IEventPublisher 
    {
        #region Private & Protected Fields
        private IQueueMessagePublisher _messagePublisher;
        private readonly IQueryIntegrationEventsHandler _queryIntegrationEvents;
        private readonly IIntegrationEventsAddCommandHandler _eventsAddCommandHandler;
        private readonly string _connectionString;

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
            using (var publishCommandsHandler = IntegrationEventPublishCommandsHandler.Create(_connectionString))
            {

                foreach (var logEvt in pendingLogEvents)
                {
                    bool eventProcessed = false;

                    try
                    {
                        // Retry logic
                        for (int attempt = 1; attempt <= 3; attempt++)
                        {
                            if (ProcessEvent(publishCommandsHandler, logEvt))
                            {
                                eventProcessed = true;
                                break; // Exit retry loop on success
                            }

                            if (attempt == 3)
                            {
                                // Mark as failed after maximum retries
                                publishCommandsHandler.MarkEventAsFailed(logEvt.EventId);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing event {logEvt.EventId}: {ex.Message}");
                        // Mark the event as failed in case of exception
                        publishCommandsHandler.MarkEventAsFailed(logEvt.EventId);
                    }

                    // Save changes after processing each event
                    try
                    {
                        await publishCommandsHandler.SaveChagesAsync();
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

            return allEventsProcessed;
        }

        private bool ProcessEvent(IIntegrationEventPublishCommandsHandler handler ,IntegrationEventDetail logEvt)
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
        private IntegrationEventDispatcher(IQueryIntegrationEventsHandler queryIntegrationEvents,
            IIntegrationEventsAddCommandHandler eventsAddCommandHandler, IQueueMessagePublisher messagePublisher, string connectionString)
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

        internal void AddRouteData(Type key, string value) {
            _routedata[key]= value;
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
        public static IntegrationEventDispatcher Create(IntegrationEventDataContext context, IQueueMessagePublisher messagePublisher, IRabbitMQConfigurationManager configurationManager)
        {
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            var qryHandler = QueryIntegrationEventsHandler.Create(connectionString);
            var addHandler = IntegrationEventsAddCommandHandler.Create(connectionString);
            var dispatcher =  new IntegrationEventDispatcher(qryHandler, addHandler, messagePublisher, connectionString);
            
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
