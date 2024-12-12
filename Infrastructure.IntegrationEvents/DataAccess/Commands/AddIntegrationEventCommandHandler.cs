using Infrastructure.IntegrationEvents.DataAccess;
using Infrastructure.IntegrationEvents.DomainModels.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.IntegrationEvents.DataAccess.Commands
{
    public class TransactionId
    {
        private TransactionId(Guid value)
        {
            IdValue = value;
        }
        public Guid IdValue { get; protected set; }

        public static TransactionId Create(Guid value)
        {
            return new(value);
        }
    }

    public interface IAddIntegrationEventCommandHandler
    {
        Task<bool> AddHeartBeatEventDataAsync();
        Task AddIntegrationEventAsync(IEnumerable<IntegrationEvent> events);
        Task AddIntegrationEventAsync(IntegrationEvent data, IDbContextTransaction transaction);
        Task AddIntegrationEventAsync(IntegrationEvent data, Guid transactionID);

        Task AddIntegrationEventAsync(IntegrationEvent data);
    }

    public class AddIntegrationEventCommandHandler : IAddIntegrationEventCommandHandler
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
        private AddIntegrationEventCommandHandler(string connectionString)
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
        public async Task AddIntegrationEventAsync(IntegrationEvent data, Guid transactionID)
        {
            if (transactionID == Guid.Empty) throw new ArgumentNullException(nameof(transactionID));
            using (var context = new IntegrationEventDataContext(_dbContextOptions, _connectionString))
            {
                var eventLogEntry = new IntegrationEventDetail(data, transactionID);
                context.EventDetails.Add(eventLogEntry);
                await context.SaveChangesAsync();
            }
        }

        public async Task AddIntegrationEventAsync(IntegrationEvent data, IDbContextTransaction transaction)
        {

            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            using (var context = new IntegrationEventDataContext(_dbContextOptions, _connectionString))
            {
                var eventLogEntry = new IntegrationEventDetail(data, transaction.TransactionId);

                context.Database.UseTransaction(transaction.GetDbTransaction());
                context.EventDetails.Add(eventLogEntry);
                await context.SaveChangesAsync();
            }
        }

        public async Task AddIntegrationEventAsync(IEnumerable<IntegrationEvent> events)
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
                            await context.SaveChangesAsync();
                        }
                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        //TODO: Add logging here, e.g., logger.LogError(ex, "Error adding heartbeat");
                        await Task.FromException(new Exception("AddIntegrationEventAsync: Failed to Save Integration Events"));
                    }
                }
            }
        }

        public async Task AddIntegrationEventAsync(IntegrationEvent data)
        {
            using (var context = new IntegrationEventDataContext(_dbContextOptions, _connectionString))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var eventLogEntry = new IntegrationEventDetail(data, transaction.TransactionId);
                        context.EventDetails.Add(eventLogEntry);
                        await context.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        //TODO: Add logging here, e.g., logger.LogError(ex, "Error adding heartbeat");
                        await Task.FromException(new Exception("AddIntegrationEventAsync(data): Failed to Save Integration Events"));
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
        public static AddIntegrationEventCommandHandler Create(IConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException("configuration cannot be null");
            return new AddIntegrationEventCommandHandler(configuration.GetConnectionString("IntegrationConnection"));
        }
        public static AddIntegrationEventCommandHandler Create(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException("Configuration cannot be null");
            return new AddIntegrationEventCommandHandler(connectionString);
        }

        #endregion
    }
}
