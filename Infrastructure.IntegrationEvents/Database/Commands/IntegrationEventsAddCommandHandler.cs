using Infrastructure.IntegrationEvents.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.IntegrationEvents.Database.Commands
{
    internal interface IIntegrationEventsAddCommandHandler
    {
        Task<bool> AddHeartBeatEventDataAsync();
        Task AddIntegrationEventAsync(IEnumerable<IntegrationEvent> events);
        Task AddIntegrationEventAsync(IntegrationEvent data, IDbContextTransaction transaction);
    }

    internal class IntegrationEventsAddCommandHandler : IIntegrationEventsAddCommandHandler
    {
        #region Constants and Static Fields
        // Add constants and static fields here
        #endregion

        #region Private & Protected Fields
        private readonly string _connectionString;
        private readonly DbContextOptions<IntegrationEventDataContext> _dbContextOptions;
        #endregion

        #region Private & Protected Methods
        #endregion

        #region Constructors
        public IntegrationEventsAddCommandHandler(string connectionString)
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
        public Task AddIntegrationEventAsync(IntegrationEvent data, IDbContextTransaction transaction)
        {

            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            using (var context = new IntegrationEventDataContext(_dbContextOptions, _connectionString))
            {
                var eventLogEntry = new IntegrationEventDetail(data, transaction.TransactionId);

                context.Database.UseTransaction(transaction.GetDbTransaction());
                context.EventDetails.Add(eventLogEntry);
                return context.SaveChangesAsync();
            }
        }

        public Task AddIntegrationEventAsync(IEnumerable<IntegrationEvent> events)
        {
            using (var context = new IntegrationEventDataContext(_dbContextOptions, _connectionString))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in events)
                        {
                            var eventLogEntry = new IntegrationEventDetail(item, transaction.TransactionId);
                            context.EventDetails.Add(eventLogEntry);
                            context.SaveChangesAsync();
                        }
                        return transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        //TODO: Add logging here, e.g., logger.LogError(ex, "Error adding heartbeat");
                        return Task.FromException(new Exception("AddIntegrationEventAsync: Failed to Save Integration Events"));
                    }
                }
            }
        }

        public async Task<bool> AddHeartBeatEventDataAsync()
        {
            using (var context = new IntegrationEventDataContext(_connectionString))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var data = new HeartBeatEvent("Pulse Check...!!");
                        var eventLogEntry = new IntegrationEventDetail(data, transaction.TransactionId);

                        context.EventDetails.Add(eventLogEntry);

                        await context.SaveChangesAsync();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        //TODO: Add logging here, e.g., logger.LogError(ex, "Error adding heartbeat");
                        return false;
                    }
                }
            }
        }

        #endregion

        #region Factory Methods
        public static IntegrationEventsAddCommandHandler Create(IConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException("configuration cannot be null");
            return new IntegrationEventsAddCommandHandler(configuration.GetConnectionString("DefaultConnection"));
        }
        public static IntegrationEventsAddCommandHandler Create(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException("Configuration cannot be null");
            return new IntegrationEventsAddCommandHandler(connectionString);
        }
        #endregion
    }
}
