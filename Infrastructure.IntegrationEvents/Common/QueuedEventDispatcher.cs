using Infrastructure.IntegrationEvents.Database;
using Infrastructure.IntegrationEvents.Entities;
using Infrastructure.IntegrationEvents.EventHandlers;
using Infrastructure.Messaging;
using Infrastructure.Messaging.Common;
using Infrastructure.Messaging.Implementation.RabbitMQ;
using System.Timers;
using Timer = System.Timers.Timer;
namespace Infrastructure.IntegrationEvents.Common
{
    public class QueuedEventDispatcher : QueuedDataProcessor<Guid>
    {
        private static Guid ALLEVENTS = Guid.Empty;
        private readonly IEventPublisher _eventPublisher;
        private readonly Timer _timer;
        private bool _isDisposed = false;
        private void OnTimedEvent(object? sender, ElapsedEventArgs e)
        {
            Console.WriteLine($"Event generated at {DateTime.Now}");
            this.AddData(ALLEVENTS);
        }
        protected override Task<bool> DispatchData(Guid transactionId)
        {
            if (ALLEVENTS.Equals(transactionId))
                return _eventPublisher.PublishAll<IntegrationEvent>();

            return _eventPublisher.Publish<IntegrationEvent>(transactionId);
        }

        public QueuedEventDispatcher(IEventPublisher eventPublisher) : base()
        {
            _eventPublisher = eventPublisher;

            _timer = new Timer(TimeSpan.FromMinutes(2).TotalMilliseconds);
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        public override void Start()
        {
            base.Start();
            _timer.Start();
        }

        public override void Stop()
        {
            _timer.Stop();
            base.Stop();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _timer?.Dispose();
                }
                _isDisposed = true;
            }
        }

        public QueuedEventDispatcher Create(string connectionString, string exchangeName, IRabbitMQConfigurationManager configurationManager)
        {
            var context = new IntegrationEventDataContext(connectionString);
            IQueueMessagePublisher publisher = RabbitMQueuePublisher.Create(exchangeName, configurationManager);
            var dispatcher = IntegrationEventDispatcher.Create(context, publisher, configurationManager);
            return new QueuedEventDispatcher(dispatcher);
        }
    }
}
