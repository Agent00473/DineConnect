using Infrastructure.IntegrationEvents.Database;
using Infrastructure.IntegrationEvents.Entities;
using Infrastructure.IntegrationEvents.EventHandlers;
using Infrastructure.Messaging;
using Infrastructure.Messaging.Common;
using Infrastructure.Messaging.Entities;
using Infrastructure.Messaging.Implementation.RabbitMQ;
using System.Timers;
using Timer = System.Timers.Timer;
namespace Infrastructure.IntegrationEvents.Common
{
    public class QueuedEventDispatcher : QueuedDataProcessor<Guid>
    {
        private const int INTERVAL_MINS = 1;
        private static Guid ALLEVENTS = Guid.Empty;
        private readonly IEventPublisher _eventPublisher;
        private readonly Timer _timer;
        private bool _isDisposed = false;
        private async void OnTimedEvent(object? sender, ElapsedEventArgs e)
        {
            Console.WriteLine($"Event generated at {DateTime.Now}");
            var result = await _eventPublisher.AddPulse<HeartBeatEvent>();
            //TODO: Use this Result to Trigger Service Down Error.
            AddData(ALLEVENTS);

        }
        protected override Task<bool> DispatchData(Guid transactionId)
        {
            try
            {
                if (ALLEVENTS.Equals(transactionId))
                    return _eventPublisher.PublishAll<IntegrationEvent>();

                return _eventPublisher.Publish<IntegrationEvent>(transactionId);
            }
            catch (Exception)
            {

                throw;
            }

        }

        private QueuedEventDispatcher(IEventPublisher eventPublisher) : base()
        {
            _eventPublisher = eventPublisher;

            _timer = new Timer(TimeSpan.FromMinutes(INTERVAL_MINS).TotalMilliseconds);
            //_timer = new Timer(30000);
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

        public override void Pause()
        {
            _timer.Stop();
            base.Pause();
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

        #region Factory Methods
        public static QueuedEventDispatcher Create(string connectionString, string exchangeName, IRabbitMQConfigurationManager configurationManager)
        {
            var context = new IntegrationEventDataContext(connectionString);
            IQueueMessagePublisher publisher = RabbitMQueuePublisher.Create(exchangeName, configurationManager);
            var dispatcher = IntegrationEventDispatcher.Create(context, publisher, configurationManager);
            return new QueuedEventDispatcher(dispatcher);
        }
        #endregion
    }
}
