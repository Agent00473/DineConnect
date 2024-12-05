using Infrastructure.IntegrationEvents.Database;
using Infrastructure.IntegrationEvents.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.IntegrationEvents
{
    internal interface IIntegrationEventManagerService
    {
        bool MarkEventAsFailed(Guid eventId);
        bool MarkEventAsInProgress(Guid eventId);
        bool MarkEventAsPublished(Guid eventId);
        Task<IEnumerable<IntegrationEventDetail>> RetrieveAllPendingEventLogsToPublishAsync();
        Task<IEnumerable<IntegrationEventDetail>> RetrievePendingEventLogsToPublishAsync(Guid transactionId);
        Task SaveIntegrationEventAsync(IntegrationEvent data, IDbContextTransaction transaction);
        Task<int> SaveChagesAsync();

    }

    internal class IntegrationEventManagerService : IIntegrationEventManagerService
    {
        #region Private & Protected Fields
        private readonly IntegrationEventDataContext _context;
        #endregion

        #region Protected & Private  Methods
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
        public IntegrationEventManagerService(IntegrationEventDataContext context)
        {
            _context = context;
        }

        /// <summary>
        ///  Constructor for non shared Integration Event Data Context
        /// </summary>
        /// <param name="configuration"></param>
        public IntegrationEventManagerService(IConfiguration configuration)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IntegrationEventDataContext>();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            _context = new IntegrationEventDataContext(optionsBuilder.Options);
        }
        #endregion

        #region Public Properties
        #endregion

        #region Public Methods
        public Task SaveIntegrationEventAsync(IntegrationEvent data, IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));

            var eventLogEntry = new IntegrationEventDetail(data, transaction.TransactionId);

            _context.Database.UseTransaction(transaction.GetDbTransaction());
            _context.EventDetails.Add(eventLogEntry);
            return _context.SaveChangesAsync();
        }

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
        public async Task<IEnumerable<IntegrationEventDetail>> RetrieveAllPendingEventLogsToPublishAsync()
        {
            count++;
            var result = await _context.EventDetails
               .Where(e => e.State == EventStateEnum.NotPublished)
               .ToListAsync();

            if (result.Count != 0)
            {
                count--;
                return result.OrderBy(o => o.CreationTime)
                    .Select(e =>
                    {
                        Type type = Type.GetType(e.EventTypeName);
                        return e.DeserializeJsonContent(type);
                    });
            }
            count--;
            return [];
        }
        static int count = 0;
        public async Task<IEnumerable<IntegrationEventDetail>> RetrievePendingEventLogsToPublishAsync(Guid transactionId)
        {
            count++;
            var result = await _context.Set<IntegrationEventDetail>()
                .Where(e => e.TransactionId == transactionId && e.State == EventStateEnum.NotPublished)
                .ToListAsync();

            if (result.Count != 0)
            {
                count--;
                return result.OrderBy(o => o.CreationTime)
                    .Select(e =>
                    {
                        Type type = Type.GetType(e.EventTypeName);
                        return e.DeserializeJsonContent(type);
                    });
            }
            count--;
            return [];
        }

        public Task<int> SaveChagesAsync()
        {
            return _context.SaveChangesAsync();
        }

        #endregion

    }
}
