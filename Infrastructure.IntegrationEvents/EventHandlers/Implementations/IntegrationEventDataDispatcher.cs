using Infrastructure.IntegrationEvents.Entities.Events;
using Infrastructure.Messaging.Common;
using System.Timers;
using Timer = System.Timers.Timer;
namespace Infrastructure.IntegrationEvents.EventHandlers.Implementations
{
    /// <summary>
    /// Manages publishing of integration Events to Message Broker 
    /// </summary>
    public class IntegrationEventDataDispatcher : QueuedDataProcessor<Guid>, IIntegrationEventDataDispatcher
    {
        private const int INTERVAL_MINS = 1;
        private static Guid ALLEVENTS = Guid.Empty;
        private readonly IEventPublisher _eventPublisher;
        private readonly Timer _timer;
        private bool _isDisposed = false;
        private async void OnTimedEvent(object? sender, ElapsedEventArgs e)
        {
            Console.WriteLine($"Event generated at {DateTime.Now}");
            //var result = await _eventPublisher.AddPulse<HeartBeatEvent>();
            //TODO: Use this Result to Trigger Service Down Error.
           // AddData(ALLEVENTS);
        }

        private IntegrationEventDataDispatcher(IEventPublisher eventPublisher) : base()
        {
            _eventPublisher = eventPublisher;

            _timer = new Timer(TimeSpan.FromMinutes(INTERVAL_MINS).TotalMilliseconds);
            //_timer = new Timer(30000);
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        protected override Task<bool> ProcessData(Guid transactionId)
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
        public static IntegrationEventDataDispatcher Create(IEventPublisher eventPublisher)
        {
            return new IntegrationEventDataDispatcher(eventPublisher);
        }
        #endregion

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
    }
}
