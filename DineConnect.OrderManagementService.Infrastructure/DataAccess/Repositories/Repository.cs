using DineConnect.OrderManagementService.Application.Interfaces;
using Infrastructure.PostgressExceptions;
using Microsoft.EntityFrameworkCore;

namespace DineConnect.OrderManagementService.Infrastructure.DataAccess.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DineOutOrderDbContext _context;
        private readonly DbSet<T> _dbSet;
        protected abstract string GetEntityName();

        private async Task<int> SaveChangesAsync(string failuremessage, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                var err = ExceptionFactory.GetDatabaseError(ex, GetEntityName(), failuremessage);
                throw err;
            }

        }

        public Repository(DineOutOrderDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
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
            await _dbSet.AddAsync(entity);
        }

        public async Task AddAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await SaveChangesAsync($"Error in Create {GetEntityName()}");
        }

        public async Task UpdateAsync(T entity)
        {

            _dbSet.Update(entity);
            await SaveChangesAsync($"Error in Update {GetEntityName()}");
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await SaveChangesAsync($"Error in Delete {GetEntityName()}");
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

    }

}
