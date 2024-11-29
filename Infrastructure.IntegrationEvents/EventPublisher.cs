using Infrastructure.IntegrationEvents.Database;
using Infrastructure.IntegrationEvents.Entities;
using Infrastructure.Messaging;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace Infrastructure.IntegrationEvents
{
    
    public class EventPublisher<TData> : IEventPublisher where TData : IntegrationEvent
    {
        #region Private & Protected Fields
        private IIntegrationEventCommandService _eventCommandService;
        private IMessagePublisher<TData> _messagePublisher;
        private QueueConfiguration _rabbitMQConfig = new QueueConfiguration("TestExchange", ExchangeType.Direct, "SampleQueue", ["Sample.Test"]);

        #endregion

        #region Protected & Private Methods
        #endregion

        #region Constructors
        // Add constructors here
        public EventPublisher(IConfiguration configuration, IMessagePublisher<TData> messagePublisher)
        {
            _eventCommandService = new IntegrationEventCommandService(configuration);
            _messagePublisher = messagePublisher;
        }

        /// <summary>
        /// Only for Test Application
        /// </summary>
        /// <param name="context"></param>
        /// <param name="messagePublisher"></param>
        public EventPublisher(IntegrationEventDataContext context, IMessagePublisher<TData> messagePublisher)
        {
            _eventCommandService = new IntegrationEventCommandService(context);
            _messagePublisher = messagePublisher;
        }

        #endregion

        #region Public Properties
        // Add public properties here
        #endregion

        #region Public Methods

        private bool ProcessEvent(IntegrationEventDetail logEvt)
        {
            _eventCommandService.MarkEventAsInProgress(logEvt.EventId);
            //TODO : Select RoutingKeys based on the Event type
            var obj = logEvt.IntegrationEvent;
            if (obj is not TData eventData)
            {
                throw new InvalidCastException($"The event data is not of type {typeof(TData).Name}");
            }

            var msg = new EventMessage<TData>
            {
                Data = eventData
            };

            if (_messagePublisher.SendMessage(_rabbitMQConfig.RoutingKeys[0], msg))
            {
                _eventCommandService.MarkEventAsPublished(logEvt.EventId);
                return true;
            }
            return false;
        }

        public async Task<bool> Publish(Guid transactionId)
        {
            var pendingLogEvents = await _eventCommandService.RetrievePendingEventLogsToPublishAsync(transactionId);
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
                            _eventCommandService.MarkEventAsFailed(logEvt.EventId);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing event {logEvt.EventId}: {ex.Message}");
                    // Mark the event as failed in case of exception
                    _eventCommandService.MarkEventAsFailed(logEvt.EventId);
                }

                // Save changes after processing each event
                try
                {
                    await _eventCommandService.SaveChagesAsync();
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

        #region Interface Implementations
        // Implement interface members here
        #endregion

        #region Factory Methods
        // Factory Method
        //public static EventPublisher Create(/* parameters */)
        //{
        //    return new EventPublisher(/* arguments */);
        //}
        #endregion
    }
}
