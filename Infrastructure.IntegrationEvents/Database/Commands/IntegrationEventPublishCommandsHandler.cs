using Infrastructure.IntegrationEvents.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.IntegrationEvents.Database.Commands
{
    internal interface IIntegrationEventPublishCommandsHandler : IDisposable
    {
        bool MarkEventAsFailed(Guid eventId);
        bool MarkEventAsInProgress(Guid eventId);
        bool MarkEventAsPublished(Guid eventId);
        public Task<int> SaveChagesAsync();
    }

    internal sealed class IntegrationEventPublishCommandsHandler : IIntegrationEventPublishCommandsHandler
    {
        #region Constants and Static Fields
        // Add constants and static fields here
        #endregion

        #region Private & Protected Fields
        private readonly string _connectionString;
        private readonly DbContextOptions<IntegrationEventDataContext> _dbContextOptions;
        private readonly IntegrationEventDataContext _context;
        private bool _disposedValue;
        #endregion


        #region Private & Protected Methods
        private bool UpdateEventState(Guid eventId, EventStateEnum status)
        {
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
        public IntegrationEventPublishCommandsHandler(string connectionString)
        {
            _connectionString = connectionString;
            var optionsBuilder = new DbContextOptionsBuilder<IntegrationEventDataContext>();
            optionsBuilder.UseNpgsql(_connectionString);
            _dbContextOptions = optionsBuilder.Options;
            _context = new IntegrationEventDataContext(_dbContextOptions, _connectionString);
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
            Dispose(disposing: true);
        }
        #endregion

        #region Factory Methods
        public static IntegrationEventPublishCommandsHandler Create(IConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException("configuration cannot be null");
            return new IntegrationEventPublishCommandsHandler(configuration.GetConnectionString("DefaultConnection"));
        }
        public static IntegrationEventPublishCommandsHandler Create(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException("Configuration cannot be null");
            return new IntegrationEventPublishCommandsHandler(connectionString);
        }
        #endregion
    }
}
