using Infrastructure.IntegrationEvents.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.IntegrationEvents.Database.Queries
{
    internal interface IIntegrationEventsQueryHandler
    {
        Task<IEnumerable<IntegrationEventDetail>> RetrieveAllPendingEventLogsToPublishAsync();
        Task<IEnumerable<IntegrationEventDetail>> RetrievePendingEventLogsToPublishAsync(Guid transactionId);
    }

    internal class IntegrationEventsQueryHandler : IIntegrationEventsQueryHandler
    {
        #region Constants and Static Fields
        #endregion

        #region Private & Protected Fields
        private readonly string _connectionString;
        private readonly DbContextOptions<IntegrationEventDataContext> _dbContextOptions;
        #endregion


        #region Private & Protected Methods
        #endregion

        #region Constructors
        public IntegrationEventsQueryHandler(string connectionString)
        {
            _connectionString = connectionString;
            var optionsBuilder = new DbContextOptionsBuilder<IntegrationEventDataContext>();
            optionsBuilder.UseNpgsql(_connectionString);
            _dbContextOptions = optionsBuilder.Options;
        }
        #endregion

        #region Public Properties
        #endregion

        #region Public Methods
        public async Task<IEnumerable<IntegrationEventDetail>> RetrieveAllPendingEventLogsToPublishAsync()
        {
            using (var context = new IntegrationEventDataContext(_dbContextOptions, _connectionString))
            {
                var result = await context.EventDetails
               .Where(e => e.State == EventStateEnum.NotPublished)
               .ToListAsync();

                if (result.Count != 0)
                {
                    return result.OrderBy(o => o.CreationTime)
                        .Select(e =>
                        {
                            Type type = Type.GetType(e.EventTypeName);
                            return e.DeserializeJsonContent(type);
                        });
                }
                return [];
            }
        }
        public async Task<IEnumerable<IntegrationEventDetail>> RetrievePendingEventLogsToPublishAsync(Guid transactionId)
        {
            using (var context = new IntegrationEventDataContext(_dbContextOptions, _connectionString))
            {
                var result = await context.Set<IntegrationEventDetail>()
                .Where(e => e.TransactionId == transactionId && e.State == EventStateEnum.NotPublished)
                .ToListAsync();
                if (result.Count != 0)
                {
                    return result.OrderBy(o => o.CreationTime)
                        .Select(e =>
                        {
                            Type type = Type.GetType(e.EventTypeName);
                            return e.DeserializeJsonContent(type);
                        });
                }
                return [];
            }
        }
        #endregion

        #region Factory Methods
        public static IntegrationEventsQueryHandler Create(IConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException("configuration cannot be null");
            return new IntegrationEventsQueryHandler(configuration.GetConnectionString("DefaultConnection"));
        }
        public static IntegrationEventsQueryHandler Create(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException("Configuration cannot be null");
            return new IntegrationEventsQueryHandler(connectionString);
        }
        #endregion
    }
}
