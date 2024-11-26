using DineConnect.OrderManagementService.Application.Common;
using DineConnect.OrderManagementService.Application.Interfaces;
using DineConnect.OrderManagementService.Domain.Customers.Events;
using DineConnect.OrderManagementService.Domain.Interfaces;
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

        protected abstract string GetEntityName();

        protected abstract void PublishEvents(T entity);
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
            if (_contextTransaction == null)
                _contextTransaction = await _context.Database.BeginTransactionAsync();
            return _contextTransaction;
        }

        private async Task CommitTransaction()
        {
            if (_contextTransaction == null) throw new Exception("No Transaction to commit");
            await _contextTransaction.CommitAsync();
        }

        private async Task RollbackTransaction()
        {
            if (_contextTransaction == null) throw new Exception("No Transaction to Rollback");
            await _contextTransaction.RollbackAsync();
        }

        protected Repository(DineOutOrderDbContext context, IMediator mediator)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _mediator = mediator;
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
                await BeginTransaction();
                //await _dbSet.AddAsync(entity);
                //await SaveChangesAsync($"Error in Create {GetEntityName()}");
                PublishEvents(entity);
                await CommitTransaction();
            }
            catch (Exception)
            {
                await RollbackTransaction();
                throw;
            }
            finally
            {
                _contextTransaction.Dispose();
                _contextTransaction = null;
            }
        }


        public async Task AddAsync(IEnumerable<T> entities)
        {
            try
            {
                await BeginTransaction();
                //await _dbSet.AddRangeAsync(entities);
                //await SaveChangesAsync($"Error in Create Entities {GetEntityName()}");
                foreach (var entity in entities)
                {
                    PublishEvents(entity);
                }
                await CommitTransaction();
            }
            catch (Exception)
            {
                await RollbackTransaction();
                throw;
            }
            finally
            {
                _contextTransaction.Dispose();
                _contextTransaction = null;
            }
        }

        public async Task UpdateAsync(T entity)
        {

            try
            {
                await BeginTransaction();
                _dbSet.Update(entity);
                await SaveChangesAsync($"Error in Update {GetEntityName()}");
                PublishEvents(entity);
                await CommitTransaction();
            }
            catch (Exception)
            {
                await RollbackTransaction();
                throw;
            }
            finally
            {
                _contextTransaction.Dispose();
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
                    PublishEvents(entity);
                    await CommitTransaction();
                }
                catch (Exception)
                {
                    await RollbackTransaction();
                    throw;
                }
                finally
                {
                    _contextTransaction.Dispose();
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
