using Infrastructure.IntegrationEvents.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.IntegrationEvents.DataAccess.Commands
{
    public interface IPublishIntegrationEventCommandHandler : IDisposable
    {
        bool MarkEventAsFailed(Guid eventId);
        bool MarkEventAsInProgress(Guid eventId);
        bool MarkEventAsPublished(Guid eventId);
        public Task<int> SaveChagesAsync();
    }

    internal sealed class PublishIntegrationEventCommandHandler : IPublishIntegrationEventCommandHandler
    {
        #region Constants and Static Fields
        #endregion

        #region Private & Protected Fields
        private readonly IntegrationEventDataContext _context;
        private bool _disposedValue;
        #endregion

        #region Private & Protected Methods
        private bool UpdateEventState(Guid eventId, EventStateEnum status)
        {
            Console.WriteLine($"********* ACCESS Context Named ************ : {_context.DebugName}");
            var eventLogEntry = _context.EventDetails.Single(ie => ie.EventId == eventId);
            return UpdateEventState(eventLogEntry, status);
        }
        private bool UpdateEventState(IntegrationEventDetail eventLogEntry, EventStateEnum status)
        {
            if (eventLogEntry == null) return false;
            eventLogEntry.State = status;

            if (status == EventStateEnum.InProgress)
                eventLogEntry.TimesSent++;
            return true;
        }
        #endregion

        #region Constructors
        public PublishIntegrationEventCommandHandler(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IntegrationEventDataContext>();
            optionsBuilder.UseNpgsql(connectionString);
            var dbContextOptions = optionsBuilder.Options;
            _context = new IntegrationEventDataContext(dbContextOptions, connectionString);
        }

        public PublishIntegrationEventCommandHandler(IntegrationEventDataContext dataContext)
        {
            _context = dataContext;
        }
        #endregion

        #region Public Properties
        #endregion

        #region Public Methods
        public bool MarkEventAsPublished(Guid eventId)
        {
            return UpdateEventState(eventId, EventStateEnum.Published);
        }

        public bool MarkEventAsInProgress(Guid eventId)
        {
            return UpdateEventState(eventId, EventStateEnum.InProgress);
        }

        public bool MarkEventAsFailed(Guid eventId)
        {
            return UpdateEventState(eventId, EventStateEnum.PublishedFailed);
        }

        public Task<int> SaveChagesAsync()
        {
            return _context.SaveChangesAsync();
        }
        #endregion

        #region IDisposible Interface
        protected void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Console.WriteLine("###### PublishIntegrationEventCommandHandler DISPOSED ...!! ######");

            Dispose(disposing: true);
        }
        #endregion

        #region Factory Methods
        public static PublishIntegrationEventCommandHandler Create(IConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException("configuration cannot be null");
            return new PublishIntegrationEventCommandHandler(configuration.GetConnectionString("DefaultConnection"));
        }
        public static PublishIntegrationEventCommandHandler Create(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException("Configuration cannot be null");
            return new PublishIntegrationEventCommandHandler(connectionString);
        }

        public static PublishIntegrationEventCommandHandler Create(IntegrationEventDataContext dataContext)
        {
            Console.WriteLine("###### PublishIntegrationEventCommandHandler Created ...!! ######");

            if (dataContext == null) throw new ArgumentNullException("dataContext cannot be null");
            return new PublishIntegrationEventCommandHandler(dataContext);
        }
        #endregion
    }
}
