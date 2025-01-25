using DineConnect.RestaurantManagementService.Domain.Catalogues.Entities;
using DineConnect.RestaurantManagementService.Domain.Resturants;
using Infrastructure.Domain;
using Microsoft.EntityFrameworkCore;

namespace DineConnect.RestaurantManagementService.Infrastructure.DataAccess.Repositories
{
    public abstract class GenericRepository<T, TId> : IGenericRepository<T, TId> where T : class
    {
        #region Constants and Static Fields
        #endregion

        #region Private & Protected Fields
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;
        #endregion

        #region Private & Protected Methods

        #endregion

        #region Constructors
        public GenericRepository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }
        #endregion

        #region Public Properties
        // Add public properties here
        #endregion

        #region Public Methods
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(TId id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(IEnumerable<T> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            await _dbSet.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TId id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException("Entity not found");
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetPaginatedDataAsync(int pageNumber, int count)
        {
            if (pageNumber <= 0) throw new ArgumentOutOfRangeException(nameof(pageNumber));
            if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));

            return await _dbSet
                .Skip((pageNumber - 1) * count)
                .Take(count)
                .ToListAsync();
        }

        public Task<IEnumerable<MenuItem>> GetMenuItemsByRestaurantIdAsync(RestaurantId restaurantId)
        {
            throw new NotImplementedException();
        }


        #endregion

        #region Factory Methods
        // Factory Method
        //public static GenericRepository Create(/* parameters */)
        //{
        //    return new GenericRepository(/* arguments */);
        //}


        #endregion
    }


}
