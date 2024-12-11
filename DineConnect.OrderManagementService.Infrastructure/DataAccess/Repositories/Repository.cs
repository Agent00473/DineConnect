using DineConnect.OrderManagementService.Application.Common;
using DineConnect.OrderManagementService.Application.Interfaces;
using Infrastructure.IntegrationEvents.Database.Commands;
using Infrastructure.PostgressExceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System.Transactions;

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

        protected abstract void PublishEvents(T entity, Guid transactionId);
        protected Task Publish<TData>(TData item) where TData : class
        {
            var model = NotificationModel<TData>.Create(item);

            return _mediator.Publish(model);
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


        private async Task<IDbContextTransaction> BeginTransaction()
        {
            //if (_contextTransaction == null)
            //    _contextTransaction = await _context.Database.BeginTransactionAsync();
            //return _contextTransaction;
            return null;

        }

        private Task CommitTransaction()
        {
            //if (_contextTransaction == null) throw new Exception("No Transaction to commit");
            //await _contextTransaction.CommitAsync();
            return Task.CompletedTask;
        }

        private Task RollbackTransaction()
        {
            //if (_contextTransaction == null) throw new Exception("No Transaction to Rollback");
            //await _contextTransaction.RollbackAsync();
            return Task.CompletedTask;

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
            //try
            //{
            //    using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            //    {
            //        // Add your entity to the DbSet (if required)
            //         await _dbSet.AddAsync(entity);
            //         await SaveChangesAsync($"Error in Create {GetEntityName()}");

            //        // Publish domain events
            //        PublishEvents(entity, null); // No explicit transaction object passed

            //        // Mark the transaction as successful
            //        transactionScope.Complete();
            //    }
            //}
            //catch (Exception)
            //{
            //    // No explicit rollback needed; TransactionScope will handle it automatically
            //    throw;
            //}

            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {

                    await BeginTransaction();
                    await _dbSet.AddAsync(entity);
                    await SaveChangesAsync($"Error in Create {GetEntityName()}");
                    PublishEvents(entity, Guid.NewGuid());
                    await CommitTransaction();
                    transactionScope.Complete();


                }
                catch (Exception)
                {
                    await RollbackTransaction();
                    throw;
                }
                finally
                {
                    _contextTransaction?.Dispose();
                    _contextTransaction = null;
                }
            }
        }


        public async Task AddAsync(IEnumerable<T> entities)
        {
            //using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            //{
            //    try
            //    {
            //        // Add the range of entities to the DbSet
            //        await _dbSet.AddRangeAsync(entities);
            //        await SaveChangesAsync($"Error in Create Entities {GetEntityName()}");

            //        // Publish events for each entity
            //        foreach (var entity in entities)
            //        {
            //            PublishEvents(entity, null); // No explicit DbContext transaction passed
            //        }

            //        // Mark the transaction as successful
            //        transactionScope.Complete();
            //    }
            //    catch
            //    {
            //        // No explicit rollback needed; TransactionScope will handle it automatically
            //        throw;
            //    }
            //}

            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    await BeginTransaction();
                    await _dbSet.AddRangeAsync(entities);
                    await SaveChangesAsync($"Error in Create Entities {GetEntityName()}");
                    var tID = Guid.NewGuid();
                    foreach (var entity in entities)
                    {
                        PublishEvents(entity, tID);
                    }
                    await CommitTransaction();
                    transactionScope.Complete();
                }
                catch (Exception)
                {
                    await RollbackTransaction();
                    throw;
                }
                finally
                {
                    _contextTransaction?.Dispose();
                    _contextTransaction = null;
                }
            }
        }

        public async Task UpdateAsync(T entity)
        {

            try
            {
                await BeginTransaction();
                _dbSet.Update(entity);
                await SaveChangesAsync($"Error in Update {GetEntityName()}");
                PublishEvents(entity, Guid.NewGuid());
                await CommitTransaction();
            }
            catch (Exception)
            {
                await RollbackTransaction();
                throw;
            }
            finally
            {
                _contextTransaction?.Dispose();
                _contextTransaction = null;
            }

        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                try
                {
                    await BeginTransaction();
                    _dbSet.Remove(entity);
                    await SaveChangesAsync($"Error in Delete {GetEntityName()}");
                    PublishEvents(entity, Guid.NewGuid());
                    await CommitTransaction();
                }
                catch (Exception)
                {
                    await RollbackTransaction();
                    throw;
                }
                finally
                {
                    _contextTransaction?.Dispose();
                    _contextTransaction = null;
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
