using Infrastructure.IntegrationEvents.Database.Commands;
using Infrastructure.IntegrationEvents.Events;
using Infrastructure.Messaging.Common;
namespace Infrastructure.IntegrationEvents.Common
{
    /// <summary>
    /// OBSELETE CLASS
    /// </summary>
    public interface IQueuedIntegrationEventDbSaver
    {
        void Start();
        void Stop();
        void AddData(IEnumerable<IntegrationEvent> data);
    }

    public class QueuedIntegrationEventDbSaver : QueuedDataProcessor<IEnumerable<IntegrationEvent>>, IQueuedIntegrationEventDbSaver
    {
        private readonly IAddIntegrationEventCommandHandler _handler;
        private bool _isDisposed = false;

        protected async override Task<bool> ProcessData(IEnumerable<IntegrationEvent> data)
        {
            try
            {
                await _handler.AddIntegrationEventAsync(data);
                return true;
            }
            catch (Exception ex)
            {
                //TODO: Add logging here, e.g., logger.LogError(ex, "Error saving integration event");
                throw;
            }

        }

        private QueuedIntegrationEventDbSaver(IAddIntegrationEventCommandHandler handler) : base()
        {
            _handler = handler;
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Stop()
        {
            base.Stop();
        }

        public override void Pause()
        {
            base.Pause();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (!_isDisposed)
            {
                if (disposing)
                {
                }
                _isDisposed = true;
            }
        }

        //public static IQueuedIntegrationEventDbSaver Create(string connectionstring)
        //{
        //    return new QueuedIntegrationEventDbSaver(new AddIntegrationEventCommandHandler(connectionstring));
        //}
    }
}
