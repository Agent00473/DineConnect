using Infrastructure.IntegrationEvents.DomainModels.Events;
using Infrastructure.IntegrationEvents.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.IntegrationEvents.DataAccess.Commands
{

    public interface ICreateIntegrationEventCommandHandler
    {
        Task<bool> AddHeartBeatEventDataAsync();
        Task AddIntegrationEventAsync(IEnumerable<IntegrationEvent> events, IDbContextTransaction transaction);
        Task AddIntegrationEventAsync(IntegrationEvent data, IDbContextTransaction transaction);

    }

    public class CreateIntegrationEventCommandHandler<TDBContext> : ICreateIntegrationEventCommandHandler where TDBContext : DbContext
    {
        #region Constants and Static Fields
        // Add constants and static fields here
        #endregion

        #region Private & Protected Fields
        private readonly TDBContext _dBContext;
        #endregion

        #region Private & Protected Methods
        #endregion

        #region Constructors

        public CreateIntegrationEventCommandHandler(TDBContext context)
        {
            _dBContext = context;
        }

        #endregion

        #region Public Properties
        #endregion

        #region Public Methods
        public Task AddIntegrationEventAsync(IntegrationEvent data, IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));

            _dBContext.Database.UseTransaction(transaction.GetDbTransaction());
            var eventLogEntry = new IntegrationEventDetail(data, transaction.TransactionId);
            _dBContext.Set<IntegrationEventDetail>().Add(eventLogEntry);
            return _dBContext.SaveChangesAsync();
        }

        public async Task AddIntegrationEventAsync(IEnumerable<IntegrationEvent> events, IDbContextTransaction transaction)
        {
            foreach (var item in events)
            {
                await AddIntegrationEventAsync(item, transaction);
            }

        }

        public async Task<bool> AddHeartBeatEventDataAsync()
        {

            using (var transaction = _dBContext.Database.BeginTransaction())
            {
                try
                {
                    var data = new HeartBeatEvent("Pulse Check...!!");
                    var eventLogEntry = new IntegrationEventDetail(data, transaction.TransactionId);
                    _dBContext.Set<IntegrationEventDetail>().Add(eventLogEntry);
                    await _dBContext.SaveChangesAsync();
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

        #endregion

        #region Factory Methods
        public static ICreateIntegrationEventCommandHandler Create(TDBContext context)
        {
            if (context == null) throw new ArgumentNullException("context cannot be null");
            return new CreateIntegrationEventCommandHandler<TDBContext>(context);
        }

        #endregion
    }
}

