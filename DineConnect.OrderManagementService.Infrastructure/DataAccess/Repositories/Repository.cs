using DineConnect.OrderManagementService.Application.Common;
using DineConnect.OrderManagementService.Application.Interfaces;
using Infrastructure.IntegrationEvents;
using Infrastructure.PostgressExceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace DineConnect.OrderManagementService.Infrastructure.DataAccess.Repositories
{
    internal delegate Task TransactionDelegate<T>(T data) where T : class;

    internal delegate ValueTask<EntityEntry<T>> EntityTransactionDelegate<T>(T data, CancellationToken cancellationToken) where T : class;


    public abstract class Repository<T> : ITransaction, IRepository<T> where T : class
    {
        protected readonly DineOutOrderDbContext _context;
        private IDbContextTransaction _contextTransaction;
        private readonly DbSet<T> _dbSet;
        protected readonly IMediator _mediator;
        protected IAddIntegrationEventCommandHandler _integrationEvents;
        protected abstract string GetEntityName();

        protected abstract Task PublishEventsAsync(T entity, Guid transactionId);
        protected async Task PublishAsync<TData>(TData item) where TData : class
        {
            var model = NotificationModel<TData>.Create(item);
            await _mediator.Publish(model);
        }

        private async Task<int> SaveChangesAsync(string failuremessage, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = _context.SaveChangesAsync(cancellationToken);
                return await result;
            }
            catch (Exception ex)
            {
                var err = ExceptionFactory.GetDatabaseError(ex, GetEntityName(), failuremessage);
                throw err;
            }
        }

        private async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_contextTransaction == null)
                _contextTransaction = await _context.Database.BeginTransactionAsync();
            return _contextTransaction;
        }

        private async Task CommitTransactionAsync()
        {
            if (_contextTransaction == null) throw new Exception("No Transaction to commit");
            await _contextTransaction.CommitAsync();
        }

        private async Task RollbackTransactionAsync()
        {
            if (_contextTransaction == null) throw new Exception("No Transaction to Rollback");
            await _contextTransaction.RollbackAsync();
        }

        protected Repository(DineOutOrderDbContext context, IMediator mediator, IAddIntegrationEventCommandHandler integrationEvents)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _mediator = mediator;
            _integrationEvents = integrationEvents;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            try
            {
                var transaction = await BeginTransactionAsync();
                await _dbSet.AddAsync(entity);
                await SaveChangesAsync($"Error in Create {GetEntityName()}");
                await PublishEventsAsync(entity, transaction.TransactionId);
                await CommitTransactionAsync();
            }
            catch (Exception)
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                if (_contextTransaction != null)
                {
                    await _contextTransaction.DisposeAsync();
                    _contextTransaction = null;
                }
            }
        }


        public async Task AddAsync(IEnumerable<T> entities)
        {
            try
            {
                var transaction = await BeginTransactionAsync();
                await _dbSet.AddRangeAsync(entities);
                await SaveChangesAsync($"Error in Create Entities {GetEntityName()}");
                foreach (var entity in entities)
                {
                    await PublishEventsAsync(entity, transaction.TransactionId);
                }
                await CommitTransactionAsync();
            }
            catch (Exception)
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                if (_contextTransaction != null)
                {
                    await _contextTransaction.DisposeAsync();
                    _contextTransaction = null;
                }
            }
        }

        public async Task UpdateAsync(T entity)
        {

            try
            {
                var transaction = await BeginTransactionAsync();
                _dbSet.Update(entity);
                await SaveChangesAsync($"Error in Update {GetEntityName()}");
                await PublishEventsAsync(entity, transaction.TransactionId);
                await CommitTransactionAsync();
            }
            catch (Exception)
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                if (_contextTransaction != null)
                {
                    await _contextTransaction.DisposeAsync();
                    _contextTransaction = null;
                }
            }

        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                try
                {
                    var transaction = await BeginTransactionAsync();
                    _dbSet.Remove(entity);
                    await SaveChangesAsync($"Error in Delete {GetEntityName()}");
                    await PublishEventsAsync(entity, transaction.TransactionId);
                    await CommitTransactionAsync();
                }
                catch (Exception)
                {
                    await RollbackTransactionAsync();
                    throw;
                }
                finally
                {
                    if (_contextTransaction != null)
                    {
                        await _contextTransaction.DisposeAsync();
                        _contextTransaction = null;
                    }
                }
            }
        }

        public async Task<IEnumerable<T>> GetPaginatedDataAsync(int pageNumber, int pageSize)
        {
            var totalRecords = await _dbSet.CountAsync();
            var data = await _dbSet.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return data;
        }

        public IDbContextTransaction GetCurrentTransaction()
        {
            return _contextTransaction;
        }
    }

}
